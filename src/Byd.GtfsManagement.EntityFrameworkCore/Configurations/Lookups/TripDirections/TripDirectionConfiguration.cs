using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.TripDirections;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: TripDirection lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: direction_id kodlarinin ayri referans satirlari olarak tutulmasini saglar.
public sealed class TripDirectionConfiguration : IEntityTypeConfiguration<TripDirection>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<TripDirection> builder)
    {
        builder.ToTable(TripDirectionConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(TripDirectionConsts.CodeIndex);
    }
}
