using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: LocationType lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: Kod 0 ile 4'un ayri referans satirlari olarak tutulmasini saglar.
public sealed class LocationTypeConfiguration : IEntityTypeConfiguration<LocationType>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<LocationType> builder)
    {
        builder.ToTable(LocationTypeConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(LocationTypeConsts.CodeIndex);
    }
}
