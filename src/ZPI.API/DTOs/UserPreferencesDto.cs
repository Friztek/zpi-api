using Newtonsoft.Json;

namespace ZPI.API.DTos;

public sealed record UserPreferencesDto(
    [property: JsonProperty(Required = Required.Always)]
    string PreferenceCurrency,
    
    [property: JsonProperty(Required = Required.Always)]
    bool WeeklyReports,
    
    [property: JsonProperty(Required = Required.Always)]
    bool AlertsOnEmail
);