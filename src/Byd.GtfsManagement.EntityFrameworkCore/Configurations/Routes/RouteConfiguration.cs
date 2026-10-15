using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Routes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Route tablosunun Feed kapsamli sahiplik, iliski ve guzergah tekillik kurallarini tanimlar.
// sistemdeki gorevi: Ayni Feed icinde route_id tekilligini ve agency bagini veritabani duzeyinde korur.
public sealed class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    // EF modeline sahiplik, iliski ve route_id tekillik kurallarini uygular.
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable(RouteConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(route => route.Feed).WithMany()
            .HasForeignKey(route => route.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        // agency_id GTFS dokumaninda tek ajans oldugunda istege baglidir; dis verilerde hata vermemesi icin optional eslenir.
        builder.HasOne(route => route.Agency).WithMany()
            .HasForeignKey(route => new { route.FeedId, route.AgencyId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(route => route.RouteType).WithMany()
            .HasForeignKey(route => route.RouteTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(route => route.ContinuousPickup).WithMany()
            .HasForeignKey(route => route.ContinuousPickupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(route => route.ContinuousDropOff).WithMany()
            .HasForeignKey(route => route.ContinuousDropOffId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(route => route.Network).WithMany()
            .HasForeignKey(route => new { route.FeedId, route.NetworkId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(route => route.CemvSupport).WithMany()
            .HasForeignKey(route => route.CemvSupportId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(route => route.AgencyId)
            .HasDatabaseName(RouteConsts.AgencyIndex);
        builder.HasIndex(route => new { route.FeedId, route.GtfsRouteId })
            .IsUnique().HasDatabaseName(RouteConsts.GtfsRouteIdIndex);
    }
}
