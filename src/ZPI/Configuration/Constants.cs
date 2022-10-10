namespace ZPI.API.Configuration;

internal static class Constants
{
    internal const string AppName = "zpi-api";
    internal static class ConfigSections
    {
        internal const string Serilog = "Serilog";
        internal const string CorsPolicy = "CorsPolicy";

        internal static class Swagger
        {
            internal const string Root = "Swagger";
            internal const string SecurityDefinition = $"{Root}:SecurityDefinition:OAuth2";
        }

        internal static class Authentication
        {
            internal const string Root = "Authentication";
            internal const string DefaultConfig = $"{Root}:DefaultConfig";
            internal const string JwtBearer = $"{Root}:JWTBearer";
        }
    }
}
