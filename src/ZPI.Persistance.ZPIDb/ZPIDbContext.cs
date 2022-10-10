
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ZPI.Persistance.Entities;

namespace ELT.Services.Audit.Persistance.AuditDb;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(ZPIDbConstants.DefaultSchema);

        modelBuilder.Entity<AssetEntity>(entity =>
        {
            entity.HasKey(e => e.Name);
        });
    }
}