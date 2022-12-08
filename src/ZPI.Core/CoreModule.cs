global using ILogger = Serilog.ILogger;
using ZPI.Core.UseCases;

namespace ZPI.Core;

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
        services.AddScoped<AddAssetValueUseCase>();
        services.AddScoped<GetUserPreferencesUseCase>();
        services.AddScoped<UpdateUserPreferencesUseCase>();
        services.AddScoped<SearchAssetValuesUseCase>();
        services.AddScoped<GetAssetValuesUseCase>();
        services.AddScoped<GetAllUserAssetsUseCase>();
        services.AddScoped<PatchUserAssetsUseCase>();
        services.AddScoped<DeleteUserAssetUseCase>();
        services.AddScoped<AddUserUseCase>();
        services.AddScoped<PatchUserUseCase>();
        services.AddScoped<SyncWalletValuesUseCase>();
        services.AddScoped<ResetUserPasswordUseCase>();
        services.AddScoped<SendRaportsDataUseCase>();
    }
}