using Newtonsoft.Json;
using NodaTime;

namespace ZPI.API.DTos;

public sealed record TransactionDto(
    [property: JsonProperty(Required = Required.Always)]
    string AssetIdentifier,

    [property: JsonProperty(Required = Required.Always)]
    double Value,
    
    [property: JsonProperty(Required = Required.Always)]
    OffsetDateTime TimeStamp
);