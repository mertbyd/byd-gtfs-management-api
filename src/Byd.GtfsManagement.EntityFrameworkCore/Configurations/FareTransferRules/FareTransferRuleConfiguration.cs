using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FareTransferRules;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareTransferRule entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ucret aktarma kurallari verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS ucret aktarma kurallari EF tablosunu esler.</summary>
public sealed class FareTransferRuleConfiguration : IEntityTypeConfiguration<FareTransferRule>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FareTransferRule> builder)
    {
        builder.ToTable(FareTransferRuleConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId })
            .HasDatabaseName(FareTransferRuleConsts.KeyIndex);
    }
}
