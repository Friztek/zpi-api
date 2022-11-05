global using ILogger = Serilog.ILogger;
using Mapster;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using ZPI.API.Configuration;
using ZPI.API.Endpoints.Assets.GetAll;
using ZPI.API.Endpoints.AssetValues.Add;
using ZPI.API.Endpoints.AssetValues.Get;
using ZPI.API.Endpoints.AssetValues.Search;
using ZPI.API.Endpoints.User.Assets.Delete;
using ZPI.API.Endpoints.User.Assets.Get;
using ZPI.API.Endpoints.User.Assets.Patch;
using ZPI.API.Endpoints.User.Preferences.Get;
using ZPI.API.Endpoints.User.Preferences.Update;
using ZPI.API.Endpoints.Users.Add;
using ZPI.API.Mappings;
using ZPI.AspNetCore.Utils;
using ZPI.IAPI.Mappings;

namespace ZPI.API;

public static class ApiModule
{
    public static void AddAPIModule(this IServiceCollection services)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger>();
        services.RegisterSwagger(logger);
        services.RegisterPresenters(logger);
        services.RegisterMappings(logger);
        services.RegisterServices(logger);
    }

    public static void RegisterServices(this IServiceCollection services, ILogger logger)
    {
        services.AddScoped<IUserInfoService, UserInfoService>();
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
        services.AddScoped<AddAssetValuePresenter>();
        services.AddScoped<GetUserPreferencesPresenter>();
        services.AddScoped<UpdateUserPreferencesPresenter>();
        services.AddScoped<SearchAssetValuesPresenter>();
        services.AddScoped<GetAssetValuesPresenter>();
        services.AddScoped<GetAllUserAssetsPresenter>();
        services.AddScoped<DeleteUserAssetsPresenter>();
        services.AddScoped<PatchUserAssetsPresenter>();
        services.AddScoped<AddUserPresenter>();
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