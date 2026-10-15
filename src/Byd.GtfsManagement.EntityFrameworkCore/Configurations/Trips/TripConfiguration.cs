using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Trips;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Trip tablosunun Feed kapsamli sahiplik, iliski ve indeks kurallarini tanimlar.
// sistemdeki gorevi: Ayni Feed icinde trip_id tekilligini veritabani duzeyinde korur.
public sealed class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    // EF modeline sahiplik, iliski ve trip_id tekillik kurallarini uygular.
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable(TripConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(trip => trip.Feed).WithMany()
            .HasForeignKey(trip => trip.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(trip => trip.Route).WithMany()
            .HasForeignKey(trip => new { trip.FeedId, trip.RouteId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(trip => trip.Service).WithMany()
            .HasForeignKey(trip => new { trip.FeedId, trip.ServiceId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(trip => trip.TripDirection).WithMany()
            .HasForeignKey(trip => trip.TripDirectionId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(trip => trip.WheelchairAccessible).WithMany()
            .HasForeignKey(trip => trip.WheelchairAccessibleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(trip => trip.BikesAllowed).WithMany()
            .HasForeignKey(trip => trip.BikesAllowedId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(trip => trip.CarsAllowed).WithMany()
            .HasForeignKey(trip => trip.CarsAllowedId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(trip => trip.RouteId)
            .HasDatabaseName(TripConsts.RouteIndex);
        builder.HasIndex(trip => trip.ServiceId)
            .HasDatabaseName(TripConsts.ServiceIndex);
        builder.HasIndex(trip => new { trip.FeedId, trip.GtfsTripId })
            .IsUnique().HasDatabaseName(TripConsts.GtfsTripIdIndex);
    }
}
