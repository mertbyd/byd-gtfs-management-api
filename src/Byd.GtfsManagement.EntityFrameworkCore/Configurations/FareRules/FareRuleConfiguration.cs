using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.FareRules;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareRule entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ucret kurali verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS ucret kurali EF tablosunu esler.</summary>
public sealed class FareRuleConfiguration : IEntityTypeConfiguration<FareRule>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<FareRule> builder)
    {
        builder.ToTable(FareRuleConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.FareAttribute)
            .WithMany()
            .HasForeignKey(value => value.FareAttributeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(value => value.Route)
            .WithMany()
            .HasForeignKey(value => value.RouteId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new { value.FeedId, value.FareAttributeId })
            .HasDatabaseName(FareRuleConsts.KeyIndex);
    }
}
