using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FareLegJoinRules;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareLegJoinRule entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ucret etabi birlestirme kurallari verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS ucret etabi birlestirme kurallari EF tablosunu esler.</summary>
public sealed class FareLegJoinRuleConfiguration : IEntityTypeConfiguration<FareLegJoinRule>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FareLegJoinRule> builder)
    {
        builder.ToTable(FareLegJoinRuleConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId })
            .HasDatabaseName(FareLegJoinRuleConsts.KeyIndex);
    }
}
