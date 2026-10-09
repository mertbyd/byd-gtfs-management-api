using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.RouteNetworks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: RouteNetwork entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS hat ag iliskisi verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS hat ag iliskisi EF tablosunu esler.</summary>
public sealed class RouteNetworkConfiguration : IEntityTypeConfiguration<RouteNetwork>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<RouteNetwork> builder)
    {
        builder.ToTable(RouteNetworkConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsRouteId })
            .HasDatabaseName(RouteNetworkConsts.KeyIndex);
    }
}
