using Newtonsoft.Json;
using NodaTime;

namespace ZPI.API.DTos;

public sealed record TransactionBodyDto(    
    [property: JsonProperty(Required = Required.Default)]
    OffsetDate? From,

    [property: JsonProperty(Required = Required.Default)]
    OffsetDate? To
);