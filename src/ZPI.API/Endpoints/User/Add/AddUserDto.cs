using Newtonsoft.Json;
using NodaTime;

namespace ZPI.API.DTos;

public sealed record AddUserDto(
    [property: JsonProperty(Required = Required.Always)]
    string UserId
);