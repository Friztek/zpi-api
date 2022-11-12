using Newtonsoft.Json;
namespace ZPI.API.DTOs
{

public sealed record AlertDto(
    [property: JsonProperty(Required = Required.Always)]
    double Value,
    
    [property: JsonProperty(Required = Required.Always)]
    string OriginAssetName,

    [property: JsonProperty(Required = Required.Always)]
    string Currency
);
}