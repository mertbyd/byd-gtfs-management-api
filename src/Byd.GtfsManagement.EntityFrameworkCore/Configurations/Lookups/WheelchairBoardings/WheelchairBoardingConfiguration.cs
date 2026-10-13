using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: WheelchairBoarding lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: Kod 0, 1 ve 2'nin ayri referans satirlari olarak tutulmasini saglar.
public sealed class WheelchairBoardingConfiguration : IEntityTypeConfiguration<WheelchairBoarding>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<WheelchairBoarding> builder)
    {
        builder.ToTable(WheelchairBoardingConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(WheelchairBoardingConsts.CodeIndex);
    }
}
