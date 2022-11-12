using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IWalletRepository:
    ISearchRepository<IWalletRepository.GetWallets, WalletModel>,
    IGetRepository<IWalletRepository.GetWallet, double>
    
    
{
    public record GetWallets(OffsetDate? From, OffsetDate? To, string UserId);
    public record GetWallet(string UserId);
}
