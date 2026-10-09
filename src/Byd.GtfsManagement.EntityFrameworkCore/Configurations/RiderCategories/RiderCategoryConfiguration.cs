using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.RiderCategories;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: RiderCategory entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS yolcu kategorisi verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS yolcu kategorisi EF tablosunu esler.</summary>
public sealed class RiderCategoryConfiguration : IEntityTypeConfiguration<RiderCategory>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<RiderCategory> builder)
    {
        builder.ToTable(RiderCategoryConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsRiderCategoryId })
            .HasDatabaseName(RiderCategoryConsts.KeyIndex);
    }
}
