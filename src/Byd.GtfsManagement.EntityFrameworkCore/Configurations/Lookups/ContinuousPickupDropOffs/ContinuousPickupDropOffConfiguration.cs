using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: ContinuousPickupDropOff lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: continuous_pickup ve continuous_drop_off kodlarinin ayri referans satirlari olarak tutulmasini saglar.
public sealed class ContinuousPickupDropOffConfiguration : IEntityTypeConfiguration<ContinuousPickupDropOff>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<ContinuousPickupDropOff> builder)
    {
        builder.ToTable(ContinuousPickupDropOffConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(ContinuousPickupDropOffConsts.CodeIndex);
    }
}
