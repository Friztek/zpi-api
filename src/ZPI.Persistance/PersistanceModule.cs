global using ILogger = Serilog.ILogger;
using Auth0.ManagementApi;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ZPI.API.Persistance;
using ZPI.AspNetCore.Utils;
using ZPI.Core.Abstraction.Repositories;
using ZPI.IPersistance.Mappings;
using ZPI.Persistance.Mappings;
using ZPI.Persistance.Repositories;
using ZPI.Persistance.ZPIDb;

namespace ZPI.Persistance;

public static class PersistanceModule
{
    public static void AddPersistanceModule(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger>();
        services.RegisterDatabases(configuration, logger);
        services.RegisterMappings(logger);
        services.RegisterRepositories(logger);
        services.RegisterAuth0Client();
    }

    public static void RegisterAuth0Client(this IServiceCollection services)
    {
        services.AddSingleton<IManagementConnection, HttpClientManagementConnection>();
    }

    public static void UsePersistanceModule(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices.GetRequiredService<ILogger>();
        app.ApplicationServices.InitializeDatabase(logger);
    }

    private static void RegisterMappings(this IServiceCollection services, ILogger logger)
    {
        services.AddSingleton<IPersistanceMapper>((_) =>
        {
            // Prepare detached config for persistance layer
            var fork = TypeAdapterConfig.GlobalSettings.Fork(f =>
            {
                // Apply registers
                f.Apply(new PersistanceMappingRegister());

                // Configure validation rules
                f.RequireExplicitMapping = true;
                f.RequireDestinationMemberSource = true;
            });

            // Validate
            fork.Compile();

            return new PersistanceMapper(fork);
        });
    }

    private static void RegisterRepositories(this IServiceCollection services, ILogger logger)
    {
        services.AddScoped<IAssetsRepository, AssetsRepository>();
        services.AddScoped<IUserPreferencesRepository, UserPreferencesRepository>();
        services.AddScoped<IAssetValuesRepository, AssetValuesRepository>();
        services.AddScoped<ITransactionepository, TransactionRepository>();
        services.AddScoped<IUserAssetsRepository, UserAssetsRepository>();
    }

    private static void RegisterDatabases(this IServiceCollection services, IConfiguration configuration, ILogger logger)
    {
        var AuditDbConfig = configuration.GetRequiredSection<PostgresDbConfiguration>(Constants.ConfigSections.Databases.ZPI);

        logger.Information("Loaded stock movement DB configuration {@AuditDbConfig}", new
        {
            HasConnectionString = !string.IsNullOrWhiteSpace(AuditDbConfig.ConnectionString),
            AuditDbConfig.DefaultDatabase,
            AuditDbConfig.EnableAutomaticMigration,
            AuditDbConfig.PostgresApiVersion,
        });

        services.AddDbContext<ZPIDbContext>(builder =>
        {
            builder.UseNpgsql(AuditDbConfig.ConnectionString, config =>
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                config.SetPostgresVersion(AuditDbConfig.PostgresApiVersion);
                config.MigrationsHistoryTable(ZPIDbConstants.MigrationsTableName, ZPIDbConstants.MigrationsTableSchema);
                config.UseNodaTime();
            });
        });
        services.AddHealthChecks()
            .AddDbContextCheck<ZPIDbContext>("Database");
    }
    private static void InitializeDatabase(this IServiceProvider servicesProvider, ILogger logger)
    {
        var configuration = servicesProvider.GetRequiredService<IConfiguration>();
        var AuditDbConfig = configuration.GetRequiredSection<PostgresDbConfiguration>(Constants.ConfigSections.Databases.ZPI);

        if (AuditDbConfig.EnableAutomaticMigration)
        {
            MigrateDatabase<ZPIDbContext>(servicesProvider, logger);
        }
        else
        {
            logger.Information($"Migration of '{typeof(ZPIDbContext).Name}' disabled (EnableAutomaticMigration is '{AuditDbConfig.EnableAutomaticMigration}').");
        }
    }

    private static void MigrateDatabase<TContext>(IServiceProvider services, ILogger logger)
            where TContext : DbContext
    {
        try
        {
            logger.Information($"Migration of '{typeof(TContext).Name}' started.");

            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();

            using (var conn = (NpgsqlConnection)context.Database.GetDbConnection())
            {
                conn.Open();
                conn.ReloadTypes();
            }

            logger.Information($"Migration of '{typeof(TContext).Name}' completed.");
        }
        catch (Exception ex)
        {
            logger.Information(ex, $"Migration of '{typeof(TContext).Name}' failed.");
            throw;
        }
    }
}