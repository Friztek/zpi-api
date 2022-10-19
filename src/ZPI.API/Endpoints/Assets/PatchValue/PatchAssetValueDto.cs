using Newtonsoft.Json;
using NodaTime;

namespace ZPI.API.DTos;

public sealed record PatchAssetValueDto(
    [property: JsonProperty(Required = Required.Always)]
    double Value,

    [property: JsonProperty(Required = Required.Always)]
    OffsetDateTime TimeStamp
);