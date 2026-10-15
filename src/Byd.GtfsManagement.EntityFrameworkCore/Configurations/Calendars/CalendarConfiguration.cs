using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Calendars;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Calendar tablosunun Feed ve servis bagini, servis basina teklik kuralini tanimlar.
// sistemdeki gorevi: Bir servisin en fazla bir haftalik deseni olmasini veritabani duzeyinde korur.
public sealed class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
{
    // EF modeline sahiplik, iliski ve servis tekillik kurallarini uygular.
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        builder.ToTable(CalendarConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.HasOne(calendar => calendar.Feed).WithMany()
            .HasForeignKey(calendar => calendar.FeedId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(calendar => calendar.Service).WithMany()
            .HasForeignKey(calendar => new { calendar.FeedId, calendar.ServiceId })
            .HasPrincipalKey(principal => new { principal.FeedId, principal.Id })
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(calendar => calendar.ServiceId)
            .IsUnique().HasDatabaseName(CalendarConsts.ServiceIndex);
    }
}
