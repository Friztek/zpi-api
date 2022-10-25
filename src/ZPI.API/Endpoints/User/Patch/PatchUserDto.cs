using Newtonsoft.Json;

namespace ZPI.API.DTos;

public sealed record PatchUserDto(
    [property: JsonProperty(Required = Required.Always)]
    double Value,

    [property: JsonProperty(Required = Required.Always)]
    TransactionType Type,

    [property: JsonProperty(Required = Required.Always)]
    string AssetName
);