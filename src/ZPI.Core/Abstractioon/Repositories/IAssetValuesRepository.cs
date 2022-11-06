using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IAssetValuesRepository :
    ISearchRepository<IAssetValuesRepository.SearchAssetValues, AssetValueModel>,
    ISearchRepository<IAssetValuesRepository.GetAssetValues, AssetValueModel>,
    IGetRepository<IAssetValuesRepository.GetAssetValue, AssetValueModel>,
    IUpdateRepository<IAssetValuesRepository.AddAssetValue, AssetValueModel>
{
    public record SearchAssetValues(string AssetName, OffsetDate? From, OffsetDate? To);
    
    public record AddAssetValue(string AssetName, double Value, OffsetDateTime TimeStamp);

    public record GetAssetValues();

    public record GetAssetValue(string AssetName);
}