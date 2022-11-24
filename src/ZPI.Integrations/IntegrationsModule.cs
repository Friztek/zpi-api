using Auth0.ManagementApi;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ZPI.Core.Abstraction.Integrations;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Integrations.Configuration;
using ZPI.Integrations.Services;

namespace ZPI.Integrations;

public static class IntegrationsModule
{
    public static void AddIntegrationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger>();
        services.RegisterServicesInternal(configuration, logger);
        services.RegisterWorkers();
    }

    private static void RegisterServicesInternal(this IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
    {
        services.AddSingleton<IManagementConnection, HttpClientManagementConnection>();
        services.Configure<ManagementApiOptions>(configuration.GetSection(ManagementApiOptions.ManagementApi));
        services.AddScoped<IAuth0ManagementTokenProvider, Auth0ManagementTokenProvider>();
        services.AddScoped<IUsersRepository, UsersRepository>();
    }

    private static void RegisterWorkers(this IServiceCollection services)
    {
        services.AddHostedService<WorkerService>();
        services.AddHostedService<ProcAlertsWorker>();
    }
}