using Newtonsoft.Json;
namespace ZPI.API.DTOs
{
    public sealed record FullWalletDto {
    [field: JsonProperty(Required = Required.Always)]
    public double TotalValue;
    [field: JsonProperty(Required = Required.Always)]
    public double CurrencyTotalValue;
    [field: JsonProperty(Required = Required.Always)]
    public double CryptoTotalValue;
    [field: JsonProperty(Required = Required.Always)]
    public double MetalTotalValue;
    };
}