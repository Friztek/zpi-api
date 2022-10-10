using Newtonsoft.Json;

namespace ZPI.API.DTos;

public sealed record AssetDto(
    [property: JsonProperty(Required = Required.Always)]
    string Name,

    [property: JsonProperty(Required = Required.Always)]
    string FriendlyName,
    
    [property: JsonProperty(Required = Required.Always)]
    string Category
);