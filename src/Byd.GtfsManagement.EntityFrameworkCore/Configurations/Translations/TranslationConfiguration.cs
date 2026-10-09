using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Translations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Translation entity'sinin EF tablosunu, iliskilerini ve indekslerini esler.
// sistemdeki gorevi: GTFS ceviri verilerini PostgreSQL modelinde baglantilariyla korur.
/// <summary>GTFS ceviri EF tablosunu esler.</summary>
public sealed class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    // EF modeline tablo, iliski ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.ToTable(TranslationConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(value => value.Feed)
            .WithMany()
            .HasForeignKey(value => value.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(value => new
        {
            value.FeedId,
            value.TableName,
            value.FieldName,
            value.Language,
            value.RecordId,
            value.RecordSubId,
            value.FieldValue
        }).HasDatabaseName(TranslationConsts.KeyIndex);
    }
}
