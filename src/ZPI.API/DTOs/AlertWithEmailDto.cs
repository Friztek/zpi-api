using Newtonsoft.Json;
namespace ZPI.API.DTOs;

public sealed record AlertWithEmailDto(
    [property: JsonProperty(Required = Required.Always)]
    double TargetValue,

    [property: JsonProperty(Required = Required.Always)]
    string OriginAssetName,

    [property: JsonProperty(Required = Required.Always)]
    string TargetCurrency,
    
    [property: JsonProperty(Required = Required.Always)]
    string Email,

    [property: JsonProperty(Required = Required.Always)]
    double CurrentValue
);
