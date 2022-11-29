using Mapster;
using ZPI.API.DTos;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;

namespace ZPI.IAPI.Mappings;

public sealed class APIMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssetModel, AssetDto>()
            .Map(d => d.Category, s => s.Category)
            .Map(d => d.Symbol, s => s.Symbol)
            .Map(d => d.FriendlyName, s => s.FriendlyName)
            .Map(d => d.Name, s => s.Name)
            .ShallowCopyForSameType(false);

        config.NewConfig<UpdateUserPreferencesDto, UserPreferencesModel>()
            .Map(d => d.AlertsOnEmail, s => s.AlertsOnEmail)
            .Map(d => d.PreferenceCurrency, s => s.PreferenceCurrency)
            .Map(d => d.WeeklyReports, s => s.WeeklyReports)
            .Map(d => d.IsDefault, s => false)
            .ShallowCopyForSameType(false);

        config.NewConfig<UserAssetModel, UserAssetDto>()
            .Map(d => d.Asset, s => s.Asset)
            .Map(d => d.OriginValue, s => s.OriginValue)
            .Map(d => d.UserCurrencyValue, s => s.UserCurrencyValue)
            .Map(d => d.Description, s => s.Description)
            .ShallowCopyForSameType(false);

        config.NewConfig<Core.Domain.OperationType, API.DTos.OperationType>()
            .TwoWays();

        config.NewConfig<PatchUserAssetsDto, IUserAssetsRepository.UserAssetTransaction>()
            .Map(d => d.AssetName, s => s.AssetName)
            .Map(d => d.Type, s => s.Type)
            .Map(d => d.Value, s => s.Value)
            .Map(d => d.Description, s => s.Description)
            .ShallowCopyForSameType(false);

        config.NewConfig<AssetValueModel, AssetValueDto>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<TransactionModel, TransactionDto>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .Map(d => d.Description, s => s.Description)
            .ShallowCopyForSameType(false);

        config.NewConfig<WalletModel, WalletDto>()
            .Map(d => d.DateStamp, s => s.DateStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<UserPreferencesModel, UserPreferencesDto>()
            .Map(d => d.AlertsOnEmail, s => s.AlertsOnEmail)
            .Map(d => d.PreferenceCurrency, s => s.PreferenceCurrency)
            .Map(d => d.WeeklyReports, s => s.WeeklyReports)
            .ShallowCopyForSameType(false);
    }
}