using Newtonsoft.Json;

namespace ELT.Common.AspNetCore.Utils;

internal sealed class JsonSerializerSettingsConfigurator
{
    private readonly Action<JsonSerializerSettings> configurator;

    internal JsonSerializerSettingsConfigurator(Action<JsonSerializerSettings> configurator)
    {
        this.configurator = configurator;
    }

    public JsonSerializerSettings ApplyTo(JsonSerializerSettings settings) => settings.Apply(this.configurator);
    public JsonSerializerSettings CreateNew() => new JsonSerializerSettings().Apply(this.configurator);

    public static JsonSerializerSettingsConfigurator Build(Action<JsonSerializerSettings> configurator) => new(configurator);
}
