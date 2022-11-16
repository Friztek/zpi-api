using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IAssetsRepository:
    ISearchRepository<IAssetsRepository.GetAllAssets, AssetModel>,
    IGetRepository<IAssetsRepository.GetAssetByName, AssetModel>
{
    public record GetAllAssets();
    public record GetAssetByName(string assetName);
}