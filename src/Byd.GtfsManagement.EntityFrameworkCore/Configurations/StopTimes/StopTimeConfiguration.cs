using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.StopTimes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: StopTime baglarini ve trip_id + stop_sequence teklik kuralini tanimlar.
// sistemdeki gorevi: GTFS satir anahtarini veritabani duzeyinde korur.
/// <summary>StopTime EF tablosunu esler.</summary>
public sealed class StopTimeConfiguration : IEntityTypeConfiguration<StopTime>
{
    // GTFS baglarini ve satir tekligini EF modeline kaydeder.
    public void Configure(EntityTypeBuilder<StopTime> builder)
    {
        builder.ToTable(StopTimeConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed).WithMany()
            .HasForeignKey(value => value.FeedId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.Trip).WithMany()
            .HasForeignKey(value => new { value.FeedId, value.TripId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.Stop).WithMany()
            .HasForeignKey(value => new { value.FeedId, value.StopId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.LocationGroup).WithMany()
            .HasForeignKey(value => new { value.FeedId, value.LocationGroupId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.PickupType).WithMany()
            .HasForeignKey(value => value.PickupTypeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.DropOffType).WithMany()
            .HasForeignKey(value => value.DropOffTypeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.ContinuousPickup).WithMany()
            .HasForeignKey(value => value.ContinuousPickupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.ContinuousDropOff).WithMany()
            .HasForeignKey(value => value.ContinuousDropOffId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.TripId, value.StopSequence })
            .IsUnique().HasDatabaseName(StopTimeConsts.TripSequenceIndex);
        builder.HasIndex(value => value.StopId).HasDatabaseName(StopTimeConsts.StopIndex);
    }
}
