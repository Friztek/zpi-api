using Newtonsoft.Json;

namespace ZPI.API.DTos;

public sealed record UserAssetDto(
    [property: JsonProperty(Required = Required.Always)]
    AssetDto Asset,

    [property: JsonProperty(Required = Required.Always)]
    double OriginValue,

    [property: JsonProperty(Required = Required.Always)]
    double UserCurrencyValue
);