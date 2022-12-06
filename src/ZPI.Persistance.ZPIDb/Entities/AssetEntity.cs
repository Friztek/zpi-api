namespace ZPI.Persistance.Entities;

public class AssetEntity
{
    public string Identifier { get; set; }
    public string FriendlyName { get; set; }
    public string Category { get; set; }
    public string? Symbol { get; set; }
    public virtual ICollection<AssetValueEntity> Values { get; set; }
    public virtual ICollection<TransactionEntity> Transactions { get; set; }
    public virtual ICollection<UserAssetEntity> UserAssets { get; set; }
}