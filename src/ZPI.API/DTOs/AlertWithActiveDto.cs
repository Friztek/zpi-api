using Newtonsoft.Json;
namespace ZPI.API.DTOs
{

public sealed record AlertWithActiveDto(

    [property: JsonProperty(Required = Required.Always)]
    int AlertId,

    [property: JsonProperty(Required = Required.Always)]
    double Value,
    
    [property: JsonProperty(Required = Required.Always)]
    string OriginAssetName,

    [property: JsonProperty(Required = Required.Always)]
    string Currency,

    [property: JsonProperty(Required = Required.Always)]
    bool Active
);
}