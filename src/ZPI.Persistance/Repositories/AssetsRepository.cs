using Microsoft.EntityFrameworkCore;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Persistance.Entities;
using ZPI.Persistance.Mappings;
using ZPI.Persistance.ZPIDb;

namespace ZPI.Persistance.Repositories;

public class AssetsRepository : IAssetsRepository
{
    private readonly ZPIDbContext context;
    private readonly IPersistanceMapper mapper;
    public AssetsRepository(ZPIDbContext context, IPersistanceMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AssetModel>> SearchAsync(IAssetsRepository.GetAllAssets searchModel)
    {
        var assets = await context.Assets.ToListAsync();

        return this.mapper.Map<IEnumerable<AssetModel>>(assets);
    }

    public async Task<AssetModel> GetAsync(IAssetsRepository.GetAssetByName searchModel)
    {
        var asset = await context.Assets.FirstOrDefaultAsync(asset => asset.Identifier == searchModel.assetName);
        if (asset is null)
        {
            throw new AssetNotFoundException(AssetNotFoundException.GenerateBaseMessage(searchModel.assetName));
        }
        return this.mapper.Map<AssetModel>(asset);
    }
}