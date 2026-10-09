using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.BookingRules;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: BookingRule entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS rezervasyon kurallari verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS rezervasyon kurallari EF tablosunu esler.</summary>
public sealed class BookingRuleConfiguration : IEntityTypeConfiguration<BookingRule>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<BookingRule> builder)
    {
        builder.ToTable(BookingRuleConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsBookingRuleId })
            .HasDatabaseName(BookingRuleConsts.KeyIndex);
    }
}
