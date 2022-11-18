using Newtonsoft.Json;
namespace ZPI.API.DTOs
{
    public sealed record AlertDto
    {
        [property: JsonProperty(Required = Required.Always)]
        public int AlertId;

        [field: JsonProperty(Required = Required.Always)]
        public double Value;

        [field: JsonProperty(Required = Required.Always)]
        public string OriginAssetName;

        [field: JsonProperty(Required = Required.Always)]
        public string Currency;

        [field: JsonProperty(Required = Required.Always)]
        public bool Active;

        [field: JsonProperty(Required = Required.Always)]
        public string OriginAssetType;
    };
}