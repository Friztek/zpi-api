using Newtonsoft.Json;
namespace ZPI.API.DTOs
{

public sealed record AddAlertDto(
    [property: JsonProperty(Required = Required.Always)]
    double Value,
    
    [property: JsonProperty(Required = Required.Always)]
    string OriginAssetName,

    [property: JsonProperty(Required = Required.Always)]
    string Currency
);
}