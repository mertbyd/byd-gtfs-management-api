using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Areas;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Area entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS bolge verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS bolge EF tablosunu esler.</summary>
public sealed class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable(AreaConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsAreaId })
            .HasDatabaseName(AreaConsts.KeyIndex);
    }
}
