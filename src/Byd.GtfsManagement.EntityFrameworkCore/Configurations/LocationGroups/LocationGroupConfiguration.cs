using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.LocationGroups;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: LocationGroup entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS konum gruplari verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS konum gruplari EF tablosunu esler.</summary>
public sealed class LocationGroupConfiguration : IEntityTypeConfiguration<LocationGroup>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<LocationGroup> builder)
    {
        builder.ToTable(LocationGroupConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsLocationGroupId })
            .HasDatabaseName(LocationGroupConsts.KeyIndex);
    }
}
