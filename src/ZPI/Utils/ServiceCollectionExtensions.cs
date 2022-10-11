using Newtonsoft.Json;

namespace ZPI.AspNetCore.Utils;

internal static class ServiceCollectionExtensions
{
    internal static JsonSerializerSettingsConfigurator AddJsonSerializerConfigurator(this IServiceCollection services, Action<JsonSerializerSettings> configurator) => AddJsonSerializerConfigurator(services, configurator, false);
    internal static JsonSerializerSettingsConfigurator AddJsonSerializerConfigurator(this IServiceCollection services, Action<JsonSerializerSettings> configurator, bool overrideDefault) => AddJsonSerializerConfigurator(services, new JsonSerializerSettingsConfigurator(configurator), overrideDefault);
    internal static JsonSerializerSettingsConfigurator AddJsonSerializerConfigurator(this IServiceCollection services, JsonSerializerSettingsConfigurator configurator, bool overrideDefault)
    {
        services.AddSingleton(configurator);

        if (overrideDefault)
        {
            JsonConvert.DefaultSettings = configurator.CreateNew;
        }

        return configurator;
    }
}
