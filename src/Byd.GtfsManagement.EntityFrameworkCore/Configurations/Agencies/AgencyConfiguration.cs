using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Agencies;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Agency tablosunun Feed kapsamli sahiplik kurallarini ve indekslerini tanimlar.
// sistemdeki gorevi: Ayni Feed icinde agency_id tekilligini veritabani duzeyinde korur.
public sealed class AgencyConfiguration : IEntityTypeConfiguration<Agency>
{
    // EF modeline sahiplik, iliski ve agency_id tekillik kurallarini uygular.
    public void Configure(EntityTypeBuilder<Agency> builder)
    {
        builder.ToTable(AgencyConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(agency => agency.Feed).WithMany()
            .HasForeignKey(agency => agency.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(agency => new { agency.FeedId, agency.GtfsAgencyId })
            .IsUnique().HasFilter(AgencyConsts.GtfsAgencyIdIndexFilter)
            .HasDatabaseName(AgencyConsts.GtfsAgencyIdIndex);
        builder.HasIndex(agency => new { agency.FeedId })
            .IsUnique().HasFilter(AgencyConsts.MissingGtfsAgencyIdIndexFilter)
            .HasDatabaseName(AgencyConsts.MissingGtfsAgencyIdIndex);
    }
}
