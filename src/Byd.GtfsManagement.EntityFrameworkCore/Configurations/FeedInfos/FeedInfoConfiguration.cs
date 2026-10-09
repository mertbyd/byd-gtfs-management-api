using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FeedInfos;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FeedInfo entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS yayimci bilgilerini PostgreSQL modelinde Feed baglantisiyla korur.
/// <summary>GTFS yayimci bilgisi EF tablosunu esler.</summary>
public sealed class FeedInfoConfiguration : IEntityTypeConfiguration<FeedInfo>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FeedInfo> builder)
    {
        builder.ToTable(FeedInfoConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => value.FeedId)
            .IsUnique()
            .HasDatabaseName(FeedInfoConsts.KeyIndex);
    }
}
