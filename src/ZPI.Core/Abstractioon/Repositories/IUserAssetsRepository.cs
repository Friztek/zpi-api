using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IUserAssetsRepository :
    ISearchRepository<IUserAssetsRepository.GetUserAssets, UserAssetModel>,
    IUpdateRepository<IUserAssetsRepository.PatchUserAssets, IEnumerable<UserAssetModel>>,
    IDeleteRepository<IUserAssetsRepository.DeleteUserAsset, UserAssetModel>
{
    public record GetUserAssets(string UserId);
    public record PatchUserAssets(string UserId, IEnumerable<UserAssetTransaction> Assets);
    public record UserAssetTransaction(string AssetName, double Value, OperationType Type);
    public record DeleteUserAsset(string UserId, string AssetName);
}