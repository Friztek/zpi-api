using Newtonsoft.Json;
namespace ZPI.API.DTOs
{
    public sealed record FullWalletDto {
    [field: JsonProperty(Required = Required.Always)]
    public double TotalValue;
    };
}