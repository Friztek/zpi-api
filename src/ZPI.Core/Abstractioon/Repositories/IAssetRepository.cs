using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IAssetsRepository :
    ISearchRepository<IAssetsRepository.GetAllAssets, AssetModel>,
    IUpdateRepository<IAssetsRepository.PatchAssetValue, AssetValueModel>
{
    public record GetAllAssets();
    public record PatchAssetValue(string AssetName, double Value, OffsetDateTime TimeStamp);
}