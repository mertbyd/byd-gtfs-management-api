using System;

namespace Byd.GtfsManagement.Dtos.CalendarDates;

// islevi: calendar_dates.txt kaydinin istemciden gelen duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ortak alan sozlesmesidir.
public abstract class CalendarDateInputDto
{
    public int ExceptionTypeId { get; set; }
    public DateOnly Date { get; set; }
}
