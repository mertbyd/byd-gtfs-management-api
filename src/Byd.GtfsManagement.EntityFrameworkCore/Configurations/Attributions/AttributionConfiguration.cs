using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Attributions;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Attribution entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS atif verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS atif EF tablosunu esler.</summary>
public sealed class AttributionConfiguration : IEntityTypeConfiguration<Attribution>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Attribution> builder)
    {
        builder.ToTable(AttributionConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.Agency)
            .WithMany()
            .HasForeignKey(value => new { value.FeedId, value.AgencyId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.Route)
            .WithMany()
            .HasForeignKey(value => new { value.FeedId, value.RouteId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.Trip)
            .WithMany()
            .HasForeignKey(value => new { value.FeedId, value.TripId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsAttributionId })
            .HasDatabaseName(AttributionConsts.KeyIndex);
    }
}
