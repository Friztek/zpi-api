public static class StartupConfigurationHelper
{
    private static readonly Lazy<bool> IsLocalLazy = new(delegate
    {
        try
        {
            return (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")) == bool.TrueString;
        }
        catch
        {
            return false;
        }
    });

    public static bool IsLocal => IsLocalLazy.Value;

    public static IConfigurationBuilder LoadStartupConfiguration(IConfigurationBuilder builder, bool forceLocal = false)
    {
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.AddJsonFile("appsettings." + (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")) + ".json", optional: true, reloadOnChange: true);
        if (IsLocal || forceLocal)
        {
            builder.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
        }
        builder.AddEnvironmentVariables();
        return builder;
    }
}