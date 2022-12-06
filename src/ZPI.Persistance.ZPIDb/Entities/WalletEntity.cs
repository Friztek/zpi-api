using NodaTime;

namespace ZPI.Persistance.Entities;

public class WalletEntity
{
    public long Identifier { get; set; }
    public string UserIdentifier { get; set; }
    public double Value { get; set; }
    public LocalDate DateStamp { get; set; }

    public virtual UserPreferencesEntity User { get; set; }
}   