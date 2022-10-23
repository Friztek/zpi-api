using Mapster;
using ZPI.API.DTos;
using ZPI.Core.Domain;

namespace ZPI.IAPI.Mappings;

public sealed class APIMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssetModel, AssetDto>()
            .Map(d => d.Category, s => s.Category)
            .Map(d => d.FriendlyName, s => s.FriendlyName)
            .Map(d => d.Name, s => s.Name)
            .ShallowCopyForSameType(false);

        config.NewConfig<UpdateUserPreferencesDto, UserPreferencesModel>()
            .Map(d => d.AlertsOnEmail, s => s.AlertsOnEmail)
            .Map(d => d.PreferenceCurrency, s => s.PreferenceCurrency)
            .Map(d => d.WeeklyReports, s => s.WeeklyReports)
            .ShallowCopyForSameType(false);

        config.NewConfig<AssetValueModel, AssetValueDto>()
            .Map(d => d.AssetIdentifier, s => s.AssetIdentifier)
            .Map(d => d.TimeStamp, s => s.TimeStamp)
            .Map(d => d.Value, s => s.Value)
            .ShallowCopyForSameType(false);

        config.NewConfig<UserPreferencesModel, UserPreferencesDto>()
            .Map(d => d.AlertsOnEmail, s => s.AlertsOnEmail)
            .Map(d => d.PreferenceCurrency, s => s.PreferenceCurrency)
            .Map(d => d.WeeklyReports, s => s.WeeklyReports)
            .ShallowCopyForSameType(false);
    }
}