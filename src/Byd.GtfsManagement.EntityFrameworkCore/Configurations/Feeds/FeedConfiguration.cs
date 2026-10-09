using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Feed tablosunun kapsam kolonunu ve ad tekilligini tanimlar.
// sistemdeki gorevi: Ayni tenant'ta ayni adli iki Feed yazilmasini veritabani duzeyinde engeller.
public sealed class FeedConfiguration : IEntityTypeConfiguration<Feed>
{
    // EF modeline kapsam, kolon ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Feed> builder)
    {
        builder.ToTable(FeedConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(feed => feed.TenantId).IsRequired();
        builder.Property(feed => feed.Name).HasMaxLength(FeedConsts.MaxNameLength);

        builder.HasIndex(feed => new { feed.TenantId, feed.Name })
            .IsUnique().HasDatabaseName(FeedConsts.ScopeNameIndex);
    }
}
