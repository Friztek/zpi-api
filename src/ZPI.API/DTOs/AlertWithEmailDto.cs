using Newtonsoft.Json;
namespace ZPI.API.DTOs
{

    public sealed record AlertWithEmailDto {
    [field: JsonProperty(Required = Required.Always)]
    public double TargetValue;
    
    [field: JsonProperty(Required = Required.Always)]
    public string OriginAssetName;
    [field: JsonProperty(Required = Required.Always)]
    public string TargetCurrency;
    [field: JsonProperty(Required = Required.Always)]
    public string Email;

    [field: JsonProperty(Required = Required.Always)]
    public double CurrentValue;
    };
}