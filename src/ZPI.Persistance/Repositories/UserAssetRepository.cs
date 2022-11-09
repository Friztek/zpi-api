using Microsoft.EntityFrameworkCore;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Persistance.Entities;
using ZPI.Persistance.Mappings;
using ZPI.Persistance.ZPIDb;

namespace ZPI.Persistance.Repositories;

public class UserAssetsRepository : IUserAssetsRepository
{
    private readonly ZPIDbContext context;
    private readonly IPersistanceMapper mapper;
    public UserAssetsRepository(ZPIDbContext context, IPersistanceMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task DeleteAsync(IUserAssetsRepository.DeleteUserAsset deleteModel)
    {
        var user = await this.context.UserPreferences.FirstOrDefaultAsync(u => u.UserId == deleteModel.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserNotFoundException.GenerateBaseMessage(deleteModel.UserId));
        }

        var userAsset = await this.context.UserAssets.FirstOrDefaultAsync(ua => ua.UserId == deleteModel.UserId && ua.AssetIdentifier == deleteModel.AssetName);

        // TODO add checking if asset exist
        // if (userAsset is null){

        // }

        this.context.UserAssets.Remove(userAsset);
        await this.context.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserAssetModel>> SearchAsync(IUserAssetsRepository.GetUserAssets searchModel)
    {
        var user = await this.context.UserPreferences.FirstOrDefaultAsync(u => u.UserId == searchModel.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserNotFoundException.GenerateBaseMessage(searchModel.UserId));
        }

        var preferenceCurrencyAsset = await this.context.AssetValues
            .OrderBy(a => a.TimeStamp)
            .FirstOrDefaultAsync(a => a.AssetIdentifier == user.PreferenceCurrency.ToLower());

        if (preferenceCurrencyAsset is null)
        {
            // TODO
            throw new Exception();
        }

        var assetValues = await this.context.AssetValuesAtm.ToListAsync();

        var userAssets = await this.context.UserAssets
                    .Include(ua => ua.Asset)
                    .Where(ua => ua.UserId == searchModel.UserId)
                    .ToListAsync();

        return userAssets.Select(userAsset => this.mapper.Map<UserAssetModel>((userAsset,
            assetValues.FirstOrDefault(val => val.AssetIdentifier == userAsset.AssetIdentifier).Value * userAsset.Value / preferenceCurrencyAsset.Value
        )));

    }

    public async Task<IEnumerable<UserAssetModel>> UpdateAsync(IUserAssetsRepository.PatchUserAssets updateModel)
    {
        var user = await this.context.UserPreferences.FirstOrDefaultAsync(u => u.UserId == updateModel.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserNotFoundException.GenerateBaseMessage(updateModel.UserId));
        }

        var assetsIdsToUpdate = updateModel.Assets.Select(a => a.AssetName);

        var userAssetsToUpdate = await this.context.UserAssets
            .Include(ua => ua.Asset)
            .Where(u => u.UserId == updateModel.UserId)
            .Where(ua => assetsIdsToUpdate
                .Contains(ua.AssetIdentifier))
            .ToListAsync();

        foreach (var assetToUpdate in userAssetsToUpdate)
        {
            var command = updateModel.Assets.First(a => a.AssetName == assetToUpdate.AssetIdentifier);

            switch (command.Type)
            {
                case OperationType.Update:
                    assetToUpdate.Value += command.Value;
                    break;
                case OperationType.Set:
                    assetToUpdate.Value = command.Value;
                    break;
            }
        }

        var existingAssets = userAssetsToUpdate.Select(ua => ua.AssetIdentifier);

        var userAssetToCreate = updateModel.Assets.Where(asset => !existingAssets.Contains(asset.AssetName)).ToList();

        foreach (var assetToCreate in userAssetToCreate)
        {
            var asset = await this.context.Assets.FirstOrDefaultAsync(a => a.Identifier == assetToCreate.AssetName);

            if (asset is null)
            {
                throw new AssetNotFoundException(AssetNotFoundException.GenerateBaseMessage(assetToCreate.AssetName));
            }

            var newAsset = new UserAssetEntity()
            {
                Asset = asset,
                AssetIdentifier = asset.Identifier,
                UserId = updateModel.UserId,
                Value = assetToCreate.Value
            };

            this.context.UserAssets.Add(newAsset);
            userAssetsToUpdate.Add(newAsset);
        }


        await this.context.SaveChangesAsync();

        var assetValues = await context.AssetValuesAtm.ToListAsync();
        var preferenceCurrency = assetValues.FirstOrDefault(a => a.AssetIdentifier == user.PreferenceCurrency);


        return userAssetsToUpdate.Select(asset => this.mapper.Map<UserAssetModel>((
            asset,
            assetValues.FirstOrDefault(val => val.AssetIdentifier == asset.AssetIdentifier).Value * asset.Value / preferenceCurrency.Value
            )));
    }
}