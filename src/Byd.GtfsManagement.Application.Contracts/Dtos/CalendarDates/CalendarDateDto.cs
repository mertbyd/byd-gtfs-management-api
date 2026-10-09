using System;

namespace Byd.GtfsManagement.Dtos.CalendarDates;

// islevi: calendar_dates.txt kaydinin gununu, istisna turunu ve bagli servisin adini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class CalendarDateDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = default!;
    public int ExceptionTypeId { get; set; }
    public string ExceptionTypeName { get; set; } = default!;
    public DateOnly Date { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
