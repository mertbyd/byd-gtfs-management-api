using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.StopAccesses;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: StopAccess lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: stop_access kodlarinin ayri referans satirlari olarak tutulmasini saglar.
public sealed class StopAccessConfiguration : IEntityTypeConfiguration<StopAccess>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<StopAccess> builder)
    {
        builder.ToTable(StopAccessConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(StopAccessConsts.CodeIndex);
    }
}
