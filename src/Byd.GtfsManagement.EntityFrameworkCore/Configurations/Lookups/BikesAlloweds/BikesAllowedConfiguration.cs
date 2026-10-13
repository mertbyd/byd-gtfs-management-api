using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.BikesAlloweds;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: BikesAllowed lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: bikes_allowed kodlarinin ayri referans satirlari olarak tutulmasini saglar.
public sealed class BikesAllowedConfiguration : IEntityTypeConfiguration<BikesAllowed>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<BikesAllowed> builder)
    {
        builder.ToTable(BikesAllowedConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(BikesAllowedConsts.CodeIndex);
    }
}
