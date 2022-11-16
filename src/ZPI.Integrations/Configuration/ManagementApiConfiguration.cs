
namespace ZPI.Integrations.Configuration;

public class ManagementApiOptions
{
    public const string ManagementApi = "ManagementApi";
    public string BasePath { get; set; } = string.Empty;
    public string TokenEndpointUrl { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}