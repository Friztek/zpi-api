using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IAssetsRepository:
    ISearchRepository<IAssetsRepository.GetAllAssets, AssetModel>
{
    public record GetAllAssets();
}