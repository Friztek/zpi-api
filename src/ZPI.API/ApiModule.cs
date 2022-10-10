global using ILogger = Serilog.ILogger;
using ELT.Common.AspNetCore.Utils;
using ELT.Services.Audit.API.Configuration;
using Mapster;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using ZPI.API.Configuration;
using ZPI.API.Endpoints.Assets.GetAll;
using ZPI.API.Mappings;
using ZPI.IAPI.Mappings;

namespace ELT.Services.Audit;

public static class ApiModule
{

    public static void AddAPIModule(this IServiceCollection services)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger>();
        services.RegisterSwagger(logger);
        services.RegisterPresenters(logger);
        services.RegisterMappings(logger);
    }

    public static void UseAPIModule(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices.GetRequiredService<ILogger>();
        app.InitializeSwagger(logger);
    }

    private static void RegisterSwagger(this IServiceCollection services, ILogger logger)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen();
        services.AddSwaggerGenNewtonsoftSupport();
    }


    private static void RegisterMappings(this IServiceCollection services, ILogger logger)
    {
        services.AddSingleton<IAPIMapper>((_) =>
        {
            var fork = TypeAdapterConfig.GlobalSettings.Fork(f =>
            {
                // Apply registers
                f.Apply(new APIMappingRegister());

                // Configure validation rules
                f.RequireExplicitMapping = true;
                f.RequireDestinationMemberSource = true;
            });

            // Validate
            fork.Compile();

            return new APIMapper(fork);
        });
    }

    private static void RegisterPresenters(this IServiceCollection services, ILogger logger)
    {
        services.AddScoped<GetAllAssetsPresenter>();
    }


    private static void InitializeSwagger(this IApplicationBuilder app, ILogger logger)
    {
        var config = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSection<SwaggerConfiguration>(Constants.ConfigSections.Swagger.Root);

        if (config == null) return;

        if (config.Enable)
        {
            logger.Information("Enabling swagger documentation");

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {

                options.SwaggerEndpoint($"/swagger/all/swagger.json", "all");

                options.DisplayOperationId();
            });
        }

        if (config.Enable && config.AutoRedirect)
        {
            logger.Information("Enabling swagger documentation auto-redirect");

            var option = new RewriteOptions();
            option.AddRedirect("^$", $"/swagger");
            app.UseRewriter(option);
        }
    }

}