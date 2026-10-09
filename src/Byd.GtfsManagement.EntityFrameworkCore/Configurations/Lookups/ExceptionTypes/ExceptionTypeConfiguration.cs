using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.ExceptionTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: ExceptionType lookup tablosunu sabit kimlik ve tekil GTFS koduyla esler.
// sistemdeki gorevi: exception_type kodlarinin ayri referans satirlari olarak tutulmasini saglar.
public sealed class ExceptionTypeConfiguration : IEntityTypeConfiguration<ExceptionType>
{
    // EF modeline lookup tablo ve kod kurallarini uygular.
    public void Configure(EntityTypeBuilder<ExceptionType> builder)
    {
        builder.ToTable(ExceptionTypeConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(value => value.Id).ValueGeneratedNever();
        builder.HasIndex(value => value.Code).IsUnique()
            .HasDatabaseName(ExceptionTypeConsts.CodeIndex);
    }
}
