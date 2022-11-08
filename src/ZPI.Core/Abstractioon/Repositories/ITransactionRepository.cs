using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface ITransactionepository :
    ISearchRepository<ITransactionepository.GetTransactions, TransactionModel>

{
    public record GetTransactions(string AssetName, OffsetDate? From, OffsetDate? To, string UserId);
}