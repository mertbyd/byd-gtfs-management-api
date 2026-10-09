using System;

namespace Byd.GtfsManagement.Models.CalendarDates;

// mirastan gelen: ExceptionTypeId ve Date.
// burada yazilan: istisnanin uygulandigi servis.
public sealed class CalendarDateCreateModel : CalendarDateInputModel
{
    public Guid ServiceId { get; set; }
}
