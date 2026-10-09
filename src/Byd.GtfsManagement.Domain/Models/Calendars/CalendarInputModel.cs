using System;

namespace Byd.GtfsManagement.Models.Calendars;

// islevi: calendar.txt kaydinin duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncellemenin ayni normalizasyon yolundan gecmesini saglar.
public abstract class CalendarInputModel
{
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
