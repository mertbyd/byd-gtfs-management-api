using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.CalendarDates;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: CalendarDate tablosunun Feed, servis ve istisna turu bagini, gun tekilligini tanimlar.
// sistemdeki gorevi: Bir servis icin ayni tarihte tek istisna bulunmasini veritabani duzeyinde korur.
public sealed class CalendarDateConfiguration : IEntityTypeConfiguration<CalendarDate>
{
    // EF modeline sahiplik, iliski ve servis-tarih tekillik kurallarini uygular.
    public void Configure(EntityTypeBuilder<CalendarDate> builder)
    {
        builder.ToTable(CalendarDateConsts.TableName, GtfsManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.HasOne(calendarDate => calendarDate.Feed).WithMany()
            .HasForeignKey(calendarDate => calendarDate.FeedId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(calendarDate => calendarDate.Service).WithMany()
            .HasForeignKey(calendarDate => calendarDate.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(calendarDate => calendarDate.ExceptionType).WithMany()
            .HasForeignKey(calendarDate => calendarDate.ExceptionTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(calendarDate => new { calendarDate.ServiceId, calendarDate.Date })
            .IsUnique().HasDatabaseName(CalendarDateConsts.ServiceDateIndex);
    }
}
