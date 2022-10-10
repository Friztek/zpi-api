global using ILogger = Serilog.ILogger;
using ZPI.Core.UseCases;

namespace ELT.Services.Audit;

public static class CoreModule
{
    public static void AddCoreModule(this IServiceCollection services)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger>();
        services.AddUseCases(logger);
    }

    private static void AddUseCases(this IServiceCollection services, ILogger logger)
    {
        services.AddScoped<GetAllAssetsUseCase>();
    }
}