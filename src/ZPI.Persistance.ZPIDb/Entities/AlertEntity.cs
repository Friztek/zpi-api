namespace ZPI.Persistance.Entities;

public class AlertEntity
{
    public int Identifier { get; set; }
    public string UserId { get; set; }
    public string TargetCurrency { get; set; }
    public string OriginAssetId { get; set; }
    public double Value { get; set; }
    public virtual AssetEntity Asset { get; set; }
}