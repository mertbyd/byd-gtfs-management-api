using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Feed tablosunun kapsam, dosya, durum ve rapor kolonlarini ve ad tekilligini tanimlar.
// sistemdeki gorevi: Eszamanli duplicate Feed yazimini veritabani duzeyinde engeller.
public sealed class FeedConfiguration : IEntityTypeConfiguration<Feed>
{
    // EF modeline kapsam, kolon ve indeks kurallarini uygular.
    public void Configure(EntityTypeBuilder<Feed> builder)
    {
        builder.ToTable(FeedConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(feed => feed.TenantId).IsRequired();
        builder.Property(feed => feed.OrganizationId).IsRequired();
        builder.Property(feed => feed.StoredFileName).HasMaxLength(FeedConsts.MaxStoredFileNameLength);
        builder.Property(feed => feed.Name).HasMaxLength(FeedConsts.MaxNameLength);
        builder.Property(feed => feed.SourceUrl).HasMaxLength(FeedConsts.MaxSourceUrlLength);
        builder.Property(feed => feed.SourceType).HasConversion<string>();
        builder.Property(feed => feed.Status).HasConversion<string>();
        builder.Property(feed => feed.FailureCode).HasMaxLength(FeedConsts.MaxFailureCodeLength);
        // Dogrulama raporu tek jsonb kolonunda tutulur; rapor yazildiktan sonra degismedigi icin referans karsilastirmasi yeterlidir.
        builder.Property(feed => feed.ValidationReport)
            .HasColumnType(FeedConsts.ValidationReportColumnType)
            .HasConversion(
                report => JsonSerializer.Serialize(report, (JsonSerializerOptions?)null),
                json => JsonSerializer.Deserialize<FeedValidationReportModel>(json, (JsonSerializerOptions?)null),
                new ValueComparer<FeedValidationReportModel?>(false));

        builder.HasIndex(feed => new { feed.TenantId, feed.OrganizationId, feed.Name })
            .IsUnique().HasDatabaseName(FeedConsts.ScopeNameIndex);
        builder.HasIndex(feed => new { feed.TenantId, feed.OrganizationId })
            .IsUnique().HasFilter("is_active = true").HasDatabaseName(FeedConsts.ActiveOrganizationIndex);
    }
}
