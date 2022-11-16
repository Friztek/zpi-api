using Newtonsoft.Json;
using NodaTime;

namespace ZPI.API.DTos;

public sealed record WalletDto(
    [property: JsonProperty(Required = Required.Always)]
    double Value,
    
    [property: JsonProperty(Required = Required.Always)]
    LocalDate DateStamp
);