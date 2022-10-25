using NodaTime;

namespace ZPI.Persistance.Entities;

public class AssetValueAtDay
{
    public string AssetIdentifier { get; set; }
    public double Value { get; set; }
    public OffsetDateTime TimeStamp { get; set; }
}