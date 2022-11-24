using NodaTime;

namespace ZPI.Persistance.Entities;

public class TransactionEntity
{
    public long Identifier { get; set; }
    public string UserIdentifier { get; set; }
    public string AssetIdentifier { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public OffsetDateTime TimeStamp { get; set; }
    public virtual AssetEntity Asset { get; set; }
}