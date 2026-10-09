using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Transfers;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Transfer baglarini ve alti parcali GTFS satir anahtarini tanimlar.
// sistemdeki gorevi: Aktarma referanslarini ve tekilligini PostgreSQL modelinde korur.
/// <summary>GTFS aktarma EF tablosunu esler.</summary>
public sealed class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    // Tablo, kapsam, bag ve birlesik GTFS anahtarini EF modeline uygular.
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable(TransferConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        ConfigureReferences(builder);
        ConfigureKey(builder);
    }

    // Aktarmanin Feed, durak, guzergah ve sefer baglarini esler.
    private static void ConfigureReferences(EntityTypeBuilder<Transfer> builder)
    {
        builder.HasOne(value => value.Feed).WithMany()
            .HasForeignKey(value => value.FeedId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(value => value.FromStop).WithMany()
            .HasForeignKey(value => value.FromStopId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(value => value.ToStop).WithMany()
            .HasForeignKey(value => value.ToStopId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(value => value.FromRoute).WithMany()
            .HasForeignKey(value => value.FromRouteId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(value => value.ToRoute).WithMany()
            .HasForeignKey(value => value.ToRouteId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(value => value.FromTrip).WithMany()
            .HasForeignKey(value => value.FromTripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(value => value.ToTrip).WithMany()
            .HasForeignKey(value => value.ToTripId).OnDelete(DeleteBehavior.Restrict);
    }

    // Bos GTFS anahtar parcaciklarini esit sayarak satir tekilligini korur.
    private static void ConfigureKey(EntityTypeBuilder<Transfer> builder)
    {
        builder.HasIndex(value => new { value.FeedId, value.FromStopId, value.ToStopId,
                value.FromTripId, value.ToTripId, value.FromRouteId, value.ToRouteId })
            .IsUnique().AreNullsDistinct(false).HasDatabaseName(TransferConsts.KeyIndex);
    }
}
