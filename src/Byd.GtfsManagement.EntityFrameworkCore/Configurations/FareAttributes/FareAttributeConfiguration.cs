using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FareAttributes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareAttribute entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ucret verilerini PostgreSQL modelinde Feed ve Agency baglantisiyla korur.
/// <summary>GTFS ucret ozelligi EF tablosunu esler.</summary>
public sealed class FareAttributeConfiguration : IEntityTypeConfiguration<FareAttribute>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FareAttribute> builder)
    {
        builder.ToTable(FareAttributeConsts.TableName, GtfsManagementDbProperties.DbSchema);
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

        builder.HasIndex(value => new { value.FeedId, value.GtfsFareId })
            .IsUnique()
            .HasDatabaseName(FareAttributeConsts.KeyIndex);
    }
}
