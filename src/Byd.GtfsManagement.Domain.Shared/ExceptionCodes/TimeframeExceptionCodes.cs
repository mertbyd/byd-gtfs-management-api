namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: timeframes.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Zaman araligi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS zaman araligi hata kodlarini tanimlar.</summary>
public static class TimeframeExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.TimeframeGroup + "0001";
    public const string TimeframeAlreadyExists = GtfsManagementErrorCodes.TimeframeGroup + "0002";
    public const string TimeframeGroupIdRequired = GtfsManagementErrorCodes.TimeframeGroup + "0003";
    public const string TimeframeGroupIdTooLong = GtfsManagementErrorCodes.TimeframeGroup + "0004";
    public const string StartTimeTooLong = GtfsManagementErrorCodes.TimeframeGroup + "0005";
    public const string EndTimeTooLong = GtfsManagementErrorCodes.TimeframeGroup + "0006";
    public const string GtfsServiceIdRequired = GtfsManagementErrorCodes.TimeframeGroup + "0007";
    public const string GtfsServiceIdTooLong = GtfsManagementErrorCodes.TimeframeGroup + "0008";
}
