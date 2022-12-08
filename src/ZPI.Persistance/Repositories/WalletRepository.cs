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

public class WalletRepository : IWalletRepository
{
    private readonly ZPIDbContext context;
    private readonly IPersistanceMapper mapper;
    public WalletRepository(ZPIDbContext context, IPersistanceMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<WalletModel>> SearchAsync(IWalletRepository.GetWallets searchModel)
    {
        var query = context.Wallets
            .Where(e => e.UserIdentifier == searchModel.UserId)
            .AsQueryable();

        var now = SystemClock.Instance.InUtc().GetCurrentDate();
        var userPreference = await this.context.UserPreferences.FirstOrDefaultAsync(pref => pref.UserId == searchModel.UserId);

        if (searchModel.From.HasValue)
        {
            query = query.Where(e => e.DateStamp >= searchModel.From.Value);
        }

        if (searchModel.To.HasValue)
        {
            query = query.Where(e => e.DateStamp <= searchModel.To.Value);
        }

        var values = await query.ToListAsync();

        if (values.Find(wallet => wallet.DateStamp == now) is not null)
        {
            values = values.Where(wallet => wallet.DateStamp != now).ToList();
            var curr = (await GetAsync(new IWalletRepository.GetWallet(searchModel.UserId, true))).total;
            values.Add(new WalletEntity() { UserIdentifier = searchModel.UserId, DateStamp = now, Value = curr });
        }

        if (userPreference?.PreferenceCurrency == "usd")
        {
            return mapper.Map<IEnumerable<WalletModel>>(values);
        }

        var assetValues = await this.context.AssetValuesAtDay
            .Where(asset => asset.AssetIdentifier == userPreference.PreferenceCurrency)
            .ToListAsync();


        return values.Select((val) => new WalletModel(
                    searchModel.UserId,
                    val.Value / assetValues.First(ass => ass.TimeStamp.Date == val.DateStamp).Value,
                    val.DateStamp
                ));
    }

    public async Task<(double total, double currency, double crypt, double metal)> GetAsync(IWalletRepository.GetWallet searchModel)
    {
        var user = await this.context.UserPreferences.FirstOrDefaultAsync(u => u.UserId == searchModel.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserNotFoundException.GenerateBaseMessage(searchModel.UserId));
        }


        var assetValues = await context.AssetValuesAtm.ToListAsync();
        var preferenceCurrencyAsset = assetValues.FirstOrDefault(a => a.AssetIdentifier == user.PreferenceCurrency);

        if (preferenceCurrencyAsset is null)
        {
            // TODO
            throw new Exception();
        }

        var userAssets = await this.context.UserAssets
                    .Include(ua => ua.Asset)
                    .Where(ua => ua.UserId == searchModel.UserId)
                    .ToListAsync();

        var assets = userAssets.Select(userAsset => this.mapper.Map<UserAssetModel>((userAsset,
            userAsset.Value
            * assetValues.FirstOrDefault(val => val.AssetIdentifier == userAsset.AssetIdentifier).Value
            / (searchModel.InUsd ? 1 : preferenceCurrencyAsset.Value)
        )));
        var all_assets = 0d;
        var currency_assets = 0d;
        var crypto_assets = 0d;
        var metal_assets = 0d;
        foreach (UserAssetModel asset in assets)
        {
            all_assets += asset.UserCurrencyValue;
            if (asset.Asset.Category == "currency")
                currency_assets += asset.UserCurrencyValue;
            if (asset.Asset.Category == "crypto")
                crypto_assets += asset.UserCurrencyValue;
            if (asset.Asset.Category == "metal")
                metal_assets += asset.UserCurrencyValue;

        }
        return (all_assets, currency_assets, crypto_assets, metal_assets);

    }

    public async Task SyncUserWallets()
    {
        var userIds = await this.context.UserPreferences
                        .Select(user => user.UserId)
                        .ToListAsync();

        var now = SystemClock.Instance.InUtc().GetCurrentDate();

        foreach (var userId in userIds)
        {
            var (total, _, _, _) = await this.GetAsync(new IWalletRepository.GetWallet(userId, true));
            var lastWallet = await this.context.Wallets.Where(wallet => wallet.UserIdentifier == userId).OrderBy(a => a.DateStamp).LastOrDefaultAsync();
            if (lastWallet is not null && now == lastWallet.DateStamp)
            {
                lastWallet.Value = total;
                this.context.Update(lastWallet);
            }
            else
            {
                var userWallet = new WalletEntity() { Value = total, UserIdentifier = userId, DateStamp = now };
                this.context.Add(userWallet);
            }
        }

        await this.context.SaveChangesAsync();
    }
}