namespace ELT.Common.AspNetCore.Utils;
public static class ConfigurationHelper
{
    public static T? GetSection<T>(this IConfiguration configuration, string sectionName) where T : class
    {
        return configuration.GetSection(sectionName).Get<T>();
    }
    public static T GetRequiredSection<T>(this IConfiguration configuration, string sectionName) where T : class
    {
        try
        {
            return configuration.GetRequiredSection(sectionName).Get<T>();
        }
        catch (InvalidOperationException innerException)
        {
            throw new ConfigurationMissingException(sectionName, innerException);
        }
        catch (Exception)
        {
            throw;
        }
    }
}