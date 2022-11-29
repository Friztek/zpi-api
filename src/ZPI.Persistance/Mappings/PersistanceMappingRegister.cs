using Mapster;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Persistance.Entities;

namespace ZPI.IPersistance.Mappings;

public sealed class PersistanceMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssetEntity, AssetModel>()
            .Map(d => d.Category, s => s.Category)
            .Map(d => d.Symbol, s => s.Symbol)
            .Map(d => d.FriendlyName, s => s.FriendlyName)
            .Map(d => d.Name, s => s.Identifier)
            .ShallowCopyForSameType(false);

        config.NewConfig<TransactionEntity, TransactionModel>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.UserIdentifier, s => s.UserIdentifier)
            .ShallowCopyForSameType(false);

        config.NewConfig<WalletEntity, WalletModel>()
            .Map(d => d.DateStamp, s => s.DateStamp)
            .Map(d => d.Value, s => s.Value)
            .Map(d => d.UserIdentifier, s => s.UserIdentifier)
            .ShallowCopyForSameType(false);

        config.NewConfig<AssetValueAtDay, AssetValueModel>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<AssetValueAtm, AssetValueModel>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<UserPreferencesEntity, UserPreferencesModel>()
            .Map(d => d.AlertsOnEmail, s => s.AlertsOnEmail)
            .Map(d => d.PreferenceCurrency, s => s.PreferenceCurrency)
            .Map(d => d.WeeklyReports, s => s.WeeklyReports)
            .ShallowCopyForSameType(false);

        config.NewConfig<AssetValueEntity, AssetValueModel>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<UserPreferencesEntity, UserPreferencesModel>()
            .Map(d => d.AlertsOnEmail, s => s.AlertsOnEmail)
            .Map(d => d.PreferenceCurrency, s => s.PreferenceCurrency)
            .Map(d => d.WeeklyReports, s => s.WeeklyReports)
            .Map(d => d.IsDefault, s => s.IsDefault)
            .ShallowCopyForSameType(false);

        config.NewConfig<(UserAssetEntity entity, double ValueInUserCurrency), UserAssetModel>()
            .Map(d => d.Asset, s => s.entity.Asset)
            .Map(d => d.OriginValue, s => s.entity.Value)
            .Map(d => d.UserCurrencyValue, s => s.ValueInUserCurrency)
            .Map(d => d.Description, s => s.entity.Description)
            .ShallowCopyForSameType(false);

        config.NewConfig<IUserPreferencesRepository.UpdateUserPreferences, UserPreferencesEntity>()
            .Map(d => d.AlertsOnEmail, s => s.UpdateModel.AlertsOnEmail)
            .Map(d => d.PreferenceCurrency, s => s.UpdateModel.PreferenceCurrency)
            .Map(d => d.WeeklyReports, s => s.UpdateModel.WeeklyReports)
            .Ignore(d => d.IsDefault)
            .ShallowCopyForSameType(false);

        config.NewConfig<IAssetValuesRepository.AddAssetValue, AssetValueEntity>()
            .Map(d => d.AssetIdentifier, s => s.AssetName)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .Ignore(d => d.Asset)
            .Ignore(d => d.Identifier);

        config.NewConfig<Core.UseCases.UpdateUserPreferencesUseCase.Input, UserPreferencesEntity>()
            .Inherits<IUserPreferencesRepository.UpdateUserPreferences, UserPreferencesEntity>();

        config.NewConfig<Core.UseCases.AddAssetValueUseCase.Input, AssetValueEntity>()
            .Inherits<IAssetValuesRepository.AddAssetValue, AssetValueEntity>();

    }
}