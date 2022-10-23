using Microsoft.EntityFrameworkCore;
using NodaTime;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Persistance.Entities;
using ZPI.Persistance.Mappings;
using ZPI.Persistance.ZPIDb;

namespace ZPI.Persistance.Repositories;

public class AssetValuesRepository : IAssetValuesRepository
{
    private readonly ZPIDbContext context;
    private readonly IPersistanceMapper mapper;
    public AssetValuesRepository(ZPIDbContext context, IPersistanceMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AssetValueModel>> SearchAsync(IAssetValuesRepository.SearchAssetValues searchModel)
    {
        var asset = await context.Assets.FirstOrDefaultAsync(asset => asset.Identifier == searchModel.AssetName);

        if (asset is null)
        {
            throw new AssetNotFoundException(AssetNotFoundException.GenerateBaseMessage(searchModel.AssetName));
        }

        var query = context.AssetValues.Where(e => e.AssetIdentifier == searchModel.AssetName).AsQueryable();

        // TODO return last record at given date
        if (searchModel.From.HasValue)
        {
            query = query.Where(e => OffsetDateTime.Comparer.Instant.Compare(e.TimeStamp, searchModel.From.Value.At(LocalTime.Midnight)) > 0);
        }

        if (searchModel.To.HasValue)
        {
            query = query.Where(e => OffsetDateTime.Comparer.Instant.Compare(e.TimeStamp, searchModel.To.Value.At(LocalTime.Midnight)) < 0);
        }

        var values = await query.ToListAsync();
        return mapper.Map<IEnumerable<AssetValueModel>>(values);
    }

    public async Task<AssetValueModel> UpdateAsync(IAssetValuesRepository.AddAssetValue updateModel)
    {
        var asset = await context.Assets.FirstOrDefaultAsync(asset => asset.Identifier == updateModel.AssetName);

        if (asset is null)
        {
            throw new AssetNotFoundException(AssetNotFoundException.GenerateBaseMessage(updateModel.AssetName));
        }

        var entity = this.mapper.Map<AssetValueEntity>(updateModel);
        var entry = context.AssetValues.Add(entity);
        await context.SaveChangesAsync();

        return mapper.Map<AssetValueModel>(entry.Entity);
    }
}