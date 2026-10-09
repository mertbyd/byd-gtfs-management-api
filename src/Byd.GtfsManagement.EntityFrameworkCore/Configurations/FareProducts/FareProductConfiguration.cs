using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FareProducts;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareProduct entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ucret urunu verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS ucret urunu EF tablosunu esler.</summary>
public sealed class FareProductConfiguration : IEntityTypeConfiguration<FareProduct>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FareProduct> builder)
    {
        builder.ToTable(FareProductConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsFareProductId })
            .HasDatabaseName(FareProductConsts.KeyIndex);
    }
}
