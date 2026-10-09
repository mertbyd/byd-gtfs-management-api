using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Frequencies;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Frequency bagini ve trip_id + start_time teklik kuralini tanimlar.
// sistemdeki gorevi: GTFS satir anahtarini veritabani duzeyinde korur.
/// <summary>Sefer sikligi EF tablosunu esler.</summary>
public sealed class FrequencyConfiguration : IEntityTypeConfiguration<Frequency>
{
    // Tablo, bag ve satir anahtarini EF modeline uygular.
    public void Configure(EntityTypeBuilder<Frequency> builder)
    {
        builder.ToTable(FrequencyConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.HasOne(value => value.Feed).WithMany()
            .HasForeignKey(value => value.FeedId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(value => value.Trip).WithMany()
            .HasForeignKey(value => value.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(value => new { value.TripId, value.StartTime })
            .IsUnique().HasDatabaseName(FrequencyConsts.TripStartIndex);
    }
}
