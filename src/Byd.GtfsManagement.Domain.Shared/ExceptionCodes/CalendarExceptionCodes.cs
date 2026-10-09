namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: calendar.txt kayitlarinin is kurali hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: Haftalik desen ihlallerini lokalize edilebilir kodlara baglar.
public static class CalendarExceptionCodes
{
    public const string ServiceRequired = GtfsManagementErrorCodes.CalendarGroup + "0001";
    public const string StartDateRequired = GtfsManagementErrorCodes.CalendarGroup + "0002";
    public const string EndDateRequired = GtfsManagementErrorCodes.CalendarGroup + "0003";
    public const string EndDateBeforeStartDate = GtfsManagementErrorCodes.CalendarGroup + "0004";
    public const string ServiceAlreadyHasCalendar = GtfsManagementErrorCodes.CalendarGroup + "0005";
}
