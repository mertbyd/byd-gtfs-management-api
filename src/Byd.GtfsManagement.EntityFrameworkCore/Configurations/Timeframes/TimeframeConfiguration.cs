using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Timeframes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Timeframe entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS zaman araligi verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS zaman araligi EF tablosunu esler.</summary>
public sealed class TimeframeConfiguration : IEntityTypeConfiguration<Timeframe>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Timeframe> builder)
    {
        builder.ToTable(TimeframeConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.TimeframeGroupId, value.GtfsServiceId })
            .HasDatabaseName(TimeframeConsts.KeyIndex);
    }
}
