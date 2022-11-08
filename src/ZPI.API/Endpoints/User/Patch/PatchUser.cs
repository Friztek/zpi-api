using Newtonsoft.Json;

namespace ZPI.API.DTos;

public sealed record PatchUser(
    [property: JsonProperty(Required = Required.Default)]
    string? FullName,
    
    [property: JsonProperty(Required = Required.Default)]
    string? Email
);