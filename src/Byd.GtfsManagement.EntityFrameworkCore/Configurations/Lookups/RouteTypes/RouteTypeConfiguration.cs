using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.RouteTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: RouteType lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: route_type kodlarinin ayri referans satirlari olarak tutulmasini saglar.
public sealed class RouteTypeConfiguration : IEntityTypeConfiguration<RouteType>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<RouteType> builder)
    {
        builder.ToTable(RouteTypeConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(RouteTypeConsts.CodeIndex);
    }
}
