using System;
using System.Runtime.Serialization;

namespace Microsoft.Extensions.Configuration;

[Serializable]
internal class ConfigurationMissingException : Exception
{
    public ConfigurationMissingException(string configKey)
        : base(GenerateMessage(configKey))
    {
    }

    public ConfigurationMissingException(string configKey, Exception innerException)
        : base(GenerateMessage(configKey), innerException)
    {
    }

    protected ConfigurationMissingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    private static string GenerateMessage(string configKey)
    {
        return "Configuration variable '" + configKey + "' is missing!";
    }
}
