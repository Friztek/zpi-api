using NodaTime;

namespace ZPI.Persistance.Entities;

public class AssetValueAtm
{
    public string AssetIdentifier { get; set; }
    public double Value { get; set; }
    public OffsetDateTime TimeStamp { get; set; }
}