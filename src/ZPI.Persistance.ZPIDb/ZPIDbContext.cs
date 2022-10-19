
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ZPI.Persistance.Entities;

namespace ZPI.Persistance.ZPIDb;

public sealed class ZPIDbContext : DbContext
{
    protected ZPIDbContext()
    { }

    public ZPIDbContext(DbContextOptions<ZPIDbContext> options)
        : base(options)
    { }

    static ZPIDbContext()
    {
        // NpgsqlConnection.GlobalTypeMapper.MapEnum<EventSeverity>($"{AuditDbConstants.DefaultSchema}.{nameof(EventSeverity)}");
    }

    public DbSet<AssetEntity> Assets { get; init; }
    public DbSet<AlertEntity> Alerts { get; init; }
    public DbSet<AssetValueEntity> AssetValues { get; init; }
    public DbSet<TransactionEntity> Transactions { get; init; }
    public DbSet<UserAssetEntity> UserAssets { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(ZPIDbConstants.DefaultSchema);

        modelBuilder.Entity<AssetEntity>(entity =>
        {
            entity.HasKey(e => e.Identifier);
            entity.HasMany(a => a.Alerts).WithOne(a => a.Asset).HasForeignKey(e => e.OriginAssetId);
            entity.HasMany(a => a.Transactions).WithOne(a => a.Asset).HasForeignKey(e => e.AssetIdentifier);
            entity.HasMany(a => a.UserAssets).WithOne(a => a.Asset).HasForeignKey(e => e.AssetIdentifier);
            entity.HasMany(a => a.Values).WithOne(a => a.Asset).HasForeignKey(e => e.AssetIdentifier);
        });

        modelBuilder.Entity<UserAssetEntity>(entity =>
        {
            entity.HasKey(e => new { e.AssetIdentifier, e.UserId });
        });

        modelBuilder.Entity<AlertEntity>(entity =>
        {
            entity.HasKey(e => new { e.Identifier, e.UserId });
        });

        modelBuilder.Entity<AssetValueEntity>(entity =>
        {
            entity.HasKey(e => new { e.Identifier });
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.Identifier);
            entity.Property(e => e.Identifier).ValueGeneratedOnAdd();
        });
    }
}