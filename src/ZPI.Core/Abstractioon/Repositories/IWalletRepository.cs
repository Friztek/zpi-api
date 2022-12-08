using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IWalletRepository :
    ISearchRepository<IWalletRepository.GetWallets, WalletModel>,
    IGetRepository<IWalletRepository.GetWallet, (double total, double currency, double crypt, double metal)>

{
    public Task SyncUserWallets();
    public record GetWallets(LocalDate? From, LocalDate? To, string UserId);
    public record GetWallet(string UserId, bool InUsd = false);

}
