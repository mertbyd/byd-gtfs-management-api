namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: frequencies.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Kodlari lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>Sefer sikligi hata kodlarini tanimlar.</summary>
public static class FrequencyExceptionCodes
{
    public const string TripRequired = GtfsManagementErrorCodes.FrequencyGroup + "0001";
    public const string StartTimeRequired = GtfsManagementErrorCodes.FrequencyGroup + "0002";
    public const string EndTimeRequired = GtfsManagementErrorCodes.FrequencyGroup + "0003";
    public const string TimeInvalid = GtfsManagementErrorCodes.FrequencyGroup + "0004";
    public const string TimeOrderInvalid = GtfsManagementErrorCodes.FrequencyGroup + "0005";
    public const string HeadwayInvalid = GtfsManagementErrorCodes.FrequencyGroup + "0006";
    public const string ExactTimesInvalid = GtfsManagementErrorCodes.FrequencyGroup + "0007";
    public const string IntervalOverlaps = GtfsManagementErrorCodes.FrequencyGroup + "0008";
}
