using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FareMedias;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareMedia entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ucret ortami verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS ucret ortami EF tablosunu esler.</summary>
public sealed class FareMediaConfiguration : IEntityTypeConfiguration<FareMedia>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FareMedia> builder)
    {
        builder.ToTable(FareMediaConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsFareMediaId })
            .HasDatabaseName(FareMediaConsts.KeyIndex);
    }
}
