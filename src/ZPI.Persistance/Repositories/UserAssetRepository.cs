using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
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

    private void AddTransactionsRange(IEnumerable<TransactionEntity> transactions)
    {
        var currentTime = SystemClock.Instance.InUtc().GetCurrentOffsetDateTime();

        this.context.Transactions.AddRange(transactions.Select(transaction =>
        {
            transaction.TimeStamp = currentTime;
            return transaction;
        }));
    }

    private void AddTransaction(TransactionEntity transaction)
    {
        var currentTime = SystemClock.Instance.InUtc().GetCurrentOffsetDateTime();
        transaction.TimeStamp = currentTime;

        this.context.Transactions.Add(transaction);
    }

    public async Task DeleteAsync(IUserAssetsRepository.DeleteUserAsset deleteModel)
    {
        var user = await this.context.UserPreferences.FirstOrDefaultAsync(u => u.UserId == deleteModel.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserNotFoundException.GenerateBaseMessage(deleteModel.UserId));
        }

        var userAssetQuery = this.context.UserAssets.Where(ua => ua.UserId == deleteModel.UserId && ua.AssetIdentifier == deleteModel.AssetName);

        if (deleteModel.Description is not null)
        {
            userAssetQuery = userAssetQuery.Where(ua => ua.Description == deleteModel.Description);
        }


        var userAssetToDelete = await userAssetQuery.ToListAsync();


        var transactions = userAssetToDelete.Select(ua => new TransactionEntity()
        {
            AssetIdentifier = deleteModel.AssetName,
            UserIdentifier = deleteModel.UserId,
            Value = -ua.Value,
            Description = ua.Description
        });

        AddTransactionsRange(transactions);

        this.context.UserAssets.RemoveRange(userAssetToDelete);

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
        var upsertedUserAssets = new List<UserAssetEntity>();

        var user = await this.context.UserPreferences.FirstOrDefaultAsync(u => u.UserId == updateModel.UserId);
        var transactions = new List<TransactionEntity>();

        if (user is null)
        {
            throw new UserNotFoundException(UserNotFoundException.GenerateBaseMessage(updateModel.UserId));
        }

        foreach (var userAssetPatch in updateModel.Assets)
        {
            var userAssetToUpdate = await this.context.UserAssets
                    .Include(ua => ua.Asset)
                    .Where(u => u.UserId == updateModel.UserId)
                    .Where(ua => ua.Description == userAssetPatch.Description)
                    .Where(ua => ua.Asset.Identifier == userAssetPatch.AssetName)
                    .FirstOrDefaultAsync();

            if (userAssetToUpdate is UserAssetEntity userAsset)
            {
                switch (userAssetPatch.Type)
                {
                    case OperationType.Update:
                        userAsset.Value += userAssetPatch.Value;
                        break;
                    case OperationType.Set:
                        userAsset.Value = userAssetPatch.Value;
                        break;
                }

                transactions.Add(new TransactionEntity()
                {
                    AssetIdentifier = userAssetPatch.AssetName,
                    UserIdentifier = updateModel.UserId,
                    Value = userAssetPatch.Type == OperationType.Update ? userAssetPatch.Value : userAssetPatch.Value - userAssetToUpdate.Value,
                    Description = userAssetPatch.Description
                });

                upsertedUserAssets.Add(userAsset);

            }
            else
            {
                var asset = await this.context.Assets.FirstOrDefaultAsync(a => a.Identifier == userAssetPatch.AssetName);

                if (asset is null)
                {
                    throw new AssetNotFoundException(AssetNotFoundException.GenerateBaseMessage(userAssetPatch.AssetName));
                }

                var newAsset = new UserAssetEntity()
                {
                    Asset = asset,
                    AssetIdentifier = asset.Identifier,
                    UserId = updateModel.UserId,
                    Value = userAssetPatch.Value,
                    Description = userAssetPatch.Description
                };

                transactions.Add(new TransactionEntity()
                {
                    AssetIdentifier = userAssetPatch.AssetName,
                    UserIdentifier = updateModel.UserId,
                    Value = userAssetPatch.Value,
                    Description = userAssetPatch.Description
                });

                this.context.Add(newAsset);

                upsertedUserAssets.Add(newAsset);
            }
        }

        AddTransactionsRange(transactions);
        await this.context.SaveChangesAsync();

        var assetValues = await context.AssetValuesAtm.ToListAsync();
        var preferenceCurrency = assetValues.FirstOrDefault(a => a.AssetIdentifier == user.PreferenceCurrency);


        return upsertedUserAssets.Select(asset => this.mapper.Map<UserAssetModel>((
            asset,
            assetValues.FirstOrDefault(val => val.AssetIdentifier == asset.AssetIdentifier).Value * asset.Value / preferenceCurrency.Value
            )));
    }
}