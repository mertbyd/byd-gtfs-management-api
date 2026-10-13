using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.LocationGroupStops;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: LocationGroupStop entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS konum grubu durak iliskisi verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS konum grubu durak iliskisi EF tablosunu esler.</summary>
public sealed class LocationGroupStopConfiguration : IEntityTypeConfiguration<LocationGroupStop>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<LocationGroupStop> builder)
    {
        builder.ToTable(LocationGroupStopConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsLocationGroupId, value.GtfsStopId })
            .HasDatabaseName(LocationGroupStopConsts.KeyIndex);
    }
}
