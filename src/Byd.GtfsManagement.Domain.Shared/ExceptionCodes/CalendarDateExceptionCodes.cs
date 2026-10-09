namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: calendar_dates.txt kayitlarinin is kurali hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: Takvim istisnasi ihlallerini lokalize edilebilir kodlara baglar.
public static class CalendarDateExceptionCodes
{
    public const string ServiceRequired = GtfsManagementErrorCodes.CalendarDateGroup + "0001";
    public const string ExceptionTypeRequired = GtfsManagementErrorCodes.CalendarDateGroup + "0002";
    public const string DateRequired = GtfsManagementErrorCodes.CalendarDateGroup + "0003";
}
