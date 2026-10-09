using System;

namespace Byd.GtfsManagement.Dtos.Calendars;

// islevi: calendar.txt kaydinin haftalik desenini, tarih araligini ve bagli servisin adini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class CalendarDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = default!;
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
