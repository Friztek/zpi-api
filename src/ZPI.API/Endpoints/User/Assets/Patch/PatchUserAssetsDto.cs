using Newtonsoft.Json;

namespace ZPI.API.DTos;

public sealed record PatchUserAssetsDto(
    [property: JsonProperty(Required = Required.Always)]
    double Value,

    [property: JsonProperty(Required = Required.Always)]
    OperationType Type,

    [property: JsonProperty(Required = Required.Always)]
    string AssetName
);