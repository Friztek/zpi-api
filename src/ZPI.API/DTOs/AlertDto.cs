using Newtonsoft.Json;
namespace ZPI.API.DTOs
{

public sealed record AlertDto{

    [field: JsonProperty(Required = Required.Always)]
    int AlertId;

    [field: JsonProperty(Required = Required.Always)]
    double Value;
    
    [field: JsonProperty(Required = Required.Always)]
    string OriginAssetName;

    [field: JsonProperty(Required = Required.Always)]
    string Currency;

    [field: JsonProperty(Required = Required.Always)]
    bool Active;

    [field: JsonProperty(Required = Required.Always)]
    string OriginAssetType;
};
}