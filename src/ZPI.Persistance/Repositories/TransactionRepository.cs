using Microsoft.EntityFrameworkCore;
using NodaTime;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Persistance.Entities;
using ZPI.Persistance.Mappings;
using ZPI.Persistance.ZPIDb;

namespace ZPI.Persistance.Repositories;

public class TransactionRepository : ITransactionepository
{
    private readonly ZPIDbContext context;
    private readonly IPersistanceMapper mapper;
    public TransactionRepository(ZPIDbContext context, IPersistanceMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TransactionModel>> SearchAsync(ITransactionepository.GetTransactions searchModel)
    {
        var query = context.Transactions
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
        return mapper.Map<IEnumerable<TransactionModel>>(values);
    }
}