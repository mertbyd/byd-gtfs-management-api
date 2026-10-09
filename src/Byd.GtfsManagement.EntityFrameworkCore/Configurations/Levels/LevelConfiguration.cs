using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Levels;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Level entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS kat verilerini PostgreSQL modelinde Feed baglantisiyla korur.
/// <summary>GTFS kat EF tablosunu esler.</summary>
public sealed class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.ToTable(LevelConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsLevelId })
            .IsUnique()
            .HasDatabaseName(LevelConsts.KeyIndex);
    }
}
