using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.PickupDropOffTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: PickupDropOffType lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: pickup_type ve drop_off_type kodlarinin ayri referans satirlari olarak tutulmasini saglar.
/// <summary>Binis-inis lookup tablosunu esler.</summary>
public sealed class PickupDropOffTypeConfiguration : IEntityTypeConfiguration<PickupDropOffType>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<PickupDropOffType> builder)
    {
        builder.ToTable(PickupDropOffTypeConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(PickupDropOffTypeConsts.CodeIndex);
    }
}
