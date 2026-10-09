using System;

namespace Byd.GtfsManagement.Dtos.CalendarDates;

// mirastan gelen: ExceptionTypeId ve Date.
// burada yazilan: istisnanin uygulandigi servis.
public sealed class CreateCalendarDateDto : CalendarDateInputDto
{
    public Guid ServiceId { get; set; }
}
