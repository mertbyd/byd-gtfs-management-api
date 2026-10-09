using System;

namespace Byd.GtfsManagement.Models.CalendarDates;

// islevi: calendar_dates.txt kaydinin duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncellemenin ayni normalizasyon yolundan gecmesini saglar.
public abstract class CalendarDateInputModel
{
    public int ExceptionTypeId { get; set; }
    public DateOnly Date { get; set; }
}
