
using Microsoft.EntityFrameworkCore;
using NodaTime;
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
    public DbSet<WalletEntity> Wallets { get; init; }

    public DbSet<UserAssetEntity> UserAssets { get; init; }
    public DbSet<UserPreferencesEntity> UserPreferences { get; init; }
    public DbSet<AssetValueAtDay> AssetValuesAtDay { get; init; }

    public DbSet<AssetValueAtm> AssetValuesAtm { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(ZPIDbConstants.DefaultSchema);

        modelBuilder.Entity<AssetEntity>().HasData(
                new AssetEntity { Identifier = "btc", FriendlyName = "Bitcoin", Category = "crypto" },
                new AssetEntity { Identifier = "gold", FriendlyName = "Gold", Category = "metal" },
                new AssetEntity { Identifier = "silver", FriendlyName = "Silver", Category = "metal" },
                new AssetEntity { Identifier = "platinum", FriendlyName = "Platinum", Category = "metal" },
                new AssetEntity { Identifier = "eth", FriendlyName = "Ethereum", Category = "crypto" },
                new AssetEntity { Identifier = "ltc", FriendlyName = "Litecoin", Category = "crypto" },
                new AssetEntity { Identifier = "eur", FriendlyName = "Euro", Category = "currency", Symbol = "€" },
                new AssetEntity { Identifier = "pln", FriendlyName = "Polish złoty", Category = "currency", Symbol = "zł" },
                new AssetEntity { Identifier = "jpy", FriendlyName = "Japanese Yen", Category = "currency", Symbol = "¥" },
                new AssetEntity { Identifier = "gbp", FriendlyName = "Pound sterling", Category = "currency", Symbol = "£" },
                new AssetEntity { Identifier = "huf", FriendlyName = "Hungarian Forint", Category = "currency", Symbol = "Ft" },
                new AssetEntity { Identifier = "try", FriendlyName = "Turkish lira", Category = "currency", Symbol = "₺" },
                new AssetEntity { Identifier = "sek", FriendlyName = "Swedish Krona", Category = "currency", Symbol = "kr" },
                new AssetEntity { Identifier = "chf", FriendlyName = "Swiss Franc", Category = "currency", Symbol = "CHf" },
                new AssetEntity { Identifier = "rub", FriendlyName = "Russian Ruble", Category = "currency", Symbol = "₽" },
                new AssetEntity { Identifier = "nok", FriendlyName = "Norwegian Krone", Category = "currency", Symbol = "kr" },
                new AssetEntity { Identifier = "cad", FriendlyName = "Canadian Dollar", Category = "currency", Symbol = "$" },
                new AssetEntity { Identifier = "inr", FriendlyName = "Indian Rupee", Category = "currency", Symbol = "₹" },
                new AssetEntity { Identifier = "czk", FriendlyName = "Czech Koruna", Category = "currency", Symbol = "Kč" },
                new AssetEntity { Identifier = "hrk", FriendlyName = "Croatian Kuna", Category = "currency", Symbol = "kn" },
                new AssetEntity { Identifier = "usd", FriendlyName = "United States Dollar", Category = "currency", Symbol = "$" }
            );

        modelBuilder.Entity<AssetValueEntity>().HasData(
            new AssetValueEntity
            {
                AssetIdentifier = "usd",
                Identifier = -1,
                Value = 1,
                TimeStamp = new OffsetDateTime()
            }
        );

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
            entity.HasKey(e => new { e.AssetIdentifier, e.UserId, e.Description });
        });

        modelBuilder.Entity<AssetValueAtDay>(entity =>
        {
            entity.ToView("AssetValueAtDay");
            entity.HasNoKey();
        });

        modelBuilder.Entity<AssetValueAtm>(entity =>
        {
            entity.ToView("AssetValueAtm");
            entity.HasNoKey();
        });

        modelBuilder.Entity<AlertEntity>(entity =>
        {
            entity.HasKey(e => new { e.Identifier, e.UserId });
        });

        modelBuilder.Entity<AssetValueEntity>(entity =>
        {
            entity.HasKey(e => new { e.Identifier });
        });

        modelBuilder.Entity<UserPreferencesEntity>(entity =>
        {
            entity.HasKey(e => e.UserId);
        });

        modelBuilder.Entity<WalletEntity>(entity =>
        {
            entity.HasKey(e => e.Identifier);
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.Identifier);
            entity.Property(e => e.Identifier).ValueGeneratedOnAdd();
        });
    }
}