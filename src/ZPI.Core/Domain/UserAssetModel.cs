namespace ZPI.Core.Domain;

public sealed record UserAssetModel(
    AssetModel Asset,
    double OriginValue,
    double UserCurrencyValue
);