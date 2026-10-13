using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Services;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Service tablosunun Feed kapsamli sahipligini ve service_id tekilligini tanimlar.
// sistemdeki gorevi: Takvim ve sefer kayitlarinin bagli oldugu kimligi veritabani duzeyinde korur.
public sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    // EF modeline sahiplik, iliski ve service_id tekillik kurallarini uygular.
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable(ServiceConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(service => service.Feed).WithMany()
            .HasForeignKey(service => service.FeedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(service => new { service.TenantId, service.FeedId, service.GtfsServiceId })
            .IsUnique().HasDatabaseName(ServiceConsts.GtfsServiceIdIndex);
    }
}
