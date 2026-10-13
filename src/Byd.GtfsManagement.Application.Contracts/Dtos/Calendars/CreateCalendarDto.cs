using System;

namespace Byd.GtfsManagement.Dtos.Calendars;

// mirastan gelen: haftalik desen ve gecerlilik araligi.
// burada yazilan: deseni tanimlayan servis.
public sealed class CreateCalendarDto : CalendarInputDto
{
    public Guid ServiceId { get; set; }
}
