using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Shapes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Shape entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS sekil noktalarini PostgreSQL modelinde Feed baglantisiyla korur.
/// <summary>GTFS sekil EF tablosunu esler.</summary>
public sealed class ShapeConfiguration : IEntityTypeConfiguration<Shape>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Shape> builder)
    {
        builder.ToTable(ShapeConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsShapeId, value.Sequence })
            .IsUnique()
            .HasDatabaseName(ShapeConsts.KeyIndex);
    }
}
