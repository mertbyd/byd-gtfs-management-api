using System;

namespace Byd.GtfsManagement.Models.Calendars;

// mirastan gelen: haftalik desen ve gecerlilik araligi.
// burada yazilan: deseni tanimlayan servis; bir servisin takvimi sonradan baska servise tasinmaz.
public sealed class CalendarCreateModel : CalendarInputModel
{
    public Guid ServiceId { get; set; }
}
