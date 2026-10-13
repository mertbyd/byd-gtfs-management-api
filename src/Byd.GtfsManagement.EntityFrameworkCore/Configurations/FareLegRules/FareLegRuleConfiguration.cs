using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FareLegRules;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareLegRule entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ucret etabi kurallari verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS ucret etabi kurallari EF tablosunu esler.</summary>
public sealed class FareLegRuleConfiguration : IEntityTypeConfiguration<FareLegRule>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FareLegRule> builder)
    {
        builder.ToTable(FareLegRuleConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.GtfsFareProductId })
            .HasDatabaseName(FareLegRuleConsts.KeyIndex);
    }
}
