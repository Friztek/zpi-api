using Microsoft.EntityFrameworkCore;
using NodaTime;
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

        if (searchModel.From.HasValue)
        {
            query = query.Where(e => OffsetDateTime.Comparer.Instant.Compare(e.TimeStamp, searchModel.From.Value.At(LocalTime.Midnight)) > 0);
        }

        if (searchModel.To.HasValue)
        {
            var dateUpper = new OffsetDate(searchModel.To.Value.Date.PlusDays(1), searchModel.To.Value.Offset);
            query = query.Where(e => OffsetDateTime.Comparer.Instant.Compare(e.TimeStamp, dateUpper.At(LocalTime.Midnight)) < 0);
        }

        var values = await query.ToListAsync();
        return mapper.Map<IEnumerable<WalletModel>>(values);
    }

    public async Task<Tuple<double, double, double, double>> GetAsync(IWalletRepository.GetWallet searchModel) {
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

        var assets = userAssets.Select(userAsset => this.mapper.Map<UserAssetModel>((userAsset,
            assetValues.FirstOrDefault(val => val.AssetIdentifier == userAsset.AssetIdentifier).Value * userAsset.Value / preferenceCurrencyAsset.Value
        )));
        
        var all_assets = 0d;
        var currency_assets = 0d;
        var crypto_assets = 0d;
        var metal_assets = 0d;
        foreach(UserAssetModel asset in assets)
        {
            all_assets += asset.UserCurrencyValue;
            if (asset.Asset.Category == "crypto")
                currency_assets += asset.UserCurrencyValue;
            if (asset.Asset.Category == "currency")
                crypto_assets += asset.UserCurrencyValue;
            if (asset.Asset.Category == "metal")
                metal_assets += asset.UserCurrencyValue;
            
        }
        return Tuple.Create(all_assets, currency_assets, crypto_assets, metal_assets);

    }
}