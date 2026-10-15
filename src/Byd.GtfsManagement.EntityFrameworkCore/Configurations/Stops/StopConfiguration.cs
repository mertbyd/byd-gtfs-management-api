using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Stops;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Stop tablosunun Feed kapsamli sahiplik, iliski ve istasyon hiyerarsisi kurallarini tanimlar.
// sistemdeki gorevi: Ayni Feed icinde stop_id tekilligini ve parent_station bagini veritabani duzeyinde korur.
public sealed class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    // EF modeline sahiplik, iliski ve stop_id tekillik kurallarini uygular.
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.ToTable(StopConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(stop => stop.Feed).WithMany()
            .HasForeignKey(stop => stop.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(stop => stop.LocationType).WithMany()
            .HasForeignKey(stop => stop.LocationTypeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(stop => stop.WheelchairBoarding).WithMany()
            .HasForeignKey(stop => stop.WheelchairBoardingId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // parent_station GTFS dokumaninda istege baglidir (parent_station_id null olabilir).
        builder.HasOne(stop => stop.ParentStation).WithMany()
            .HasForeignKey(stop => new { stop.FeedId, stop.ParentStationId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(stop => stop.Level).WithMany()
            .HasForeignKey(stop => new { stop.FeedId, stop.LevelId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(stop => stop.StopAccess).WithMany()
            .HasForeignKey(stop => stop.StopAccessId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(stop => stop.ParentStationId)
            .HasDatabaseName(StopConsts.ParentStationIndex);
        builder.HasIndex(stop => new { stop.FeedId, stop.GtfsStopId })
            .IsUnique().HasDatabaseName(StopConsts.GtfsStopIdIndex);
    }
}
