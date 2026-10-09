namespace Byd.GtfsManagement.Consts;

// islevi: calendar_dates.txt kayitlarinin tablo ve tekillik indeks adlarini tanimlar.
// sistemdeki gorevi: Bir servis icin ayni tarihte tek istisna bulunmasini sabitler.
public static class CalendarDateConsts
{
    public const string TableName = "CalendarDates";
    public const string ServiceDateIndex = "IX_CalendarDates_ServiceId_Date";
}
