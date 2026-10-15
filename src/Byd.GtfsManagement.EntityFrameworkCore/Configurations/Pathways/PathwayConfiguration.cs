using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Pathways;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Pathway entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS yuruyus yolu verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS yuruyus yolu EF tablosunu esler.</summary>
public sealed class PathwayConfiguration : IEntityTypeConfiguration<Pathway>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Pathway> builder)
    {
        builder.ToTable(PathwayConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.FromStop)
            .WithMany()
            .HasForeignKey(value => new { value.FeedId, value.FromStopId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.ToStop)
            .WithMany()
            .HasForeignKey(value => new { value.FeedId, value.ToStopId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsPathwayId })
            .IsUnique()
            .HasDatabaseName(PathwayConsts.KeyIndex);
    }
}
