using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.StopAreas;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: StopArea entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS durak bolge iliskisi verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS durak bolge iliskisi EF tablosunu esler.</summary>
public sealed class StopAreaConfiguration : IEntityTypeConfiguration<StopArea>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<StopArea> builder)
    {
        builder.ToTable(StopAreaConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsAreaId, value.GtfsStopId })
            .HasDatabaseName(StopAreaConsts.KeyIndex);
    }
}
