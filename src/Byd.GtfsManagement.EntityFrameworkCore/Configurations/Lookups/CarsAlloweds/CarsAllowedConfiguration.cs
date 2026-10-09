using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.CarsAlloweds;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: CarsAllowed lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: cars_allowed kodlarinin ayri referans satirlari olarak tutulmasini saglar.
public sealed class CarsAllowedConfiguration : IEntityTypeConfiguration<CarsAllowed>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<CarsAllowed> builder)
    {
        builder.ToTable(CarsAllowedConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(CarsAllowedConsts.CodeIndex);
    }
}
