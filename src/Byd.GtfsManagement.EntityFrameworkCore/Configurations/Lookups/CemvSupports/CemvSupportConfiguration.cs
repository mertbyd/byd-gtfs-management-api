using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: cEMV lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: Kod 0, 1 ve 2'nin ayri referans satirlari olarak tutulmasini saglar.
public sealed class CemvSupportConfiguration : IEntityTypeConfiguration<CemvSupport>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<CemvSupport> builder)
    {
        builder.ToTable(CemvSupportConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(CemvSupportConsts.CodeIndex);
    }
}
