namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: stop_times.txt is kurali ve girdi hata kodlarini tanimlar.
// sistemdeki gorevi: GTFS alan ihlallerini lokalize edilebilir kodlara baglar.
/// <summary>StopTime hata kodlarini tanimlar.</summary>
public static class StopTimeExceptionCodes
{
    public const string TripRequired = GtfsManagementErrorCodes.StopTimeGroup + "0001";
    public const string StopRequired = GtfsManagementErrorCodes.StopTimeGroup + "0002";
    public const string StopSequenceInvalid = GtfsManagementErrorCodes.StopTimeGroup + "0003";
    public const string StopOutOfFeed = GtfsManagementErrorCodes.StopTimeGroup + "0004";
    public const string StopNotPlatform = GtfsManagementErrorCodes.StopTimeGroup + "0005";
    public const string TimeInvalid = GtfsManagementErrorCodes.StopTimeGroup + "0006";
    public const string TimeRequired = GtfsManagementErrorCodes.StopTimeGroup + "0007";
    public const string TimeForbidden = GtfsManagementErrorCodes.StopTimeGroup + "0008";
    public const string WindowInvalid = GtfsManagementErrorCodes.StopTimeGroup + "0009";
    public const string ShapeDistanceInvalid = GtfsManagementErrorCodes.StopTimeGroup + "0010";
    public const string TimepointInvalid = GtfsManagementErrorCodes.StopTimeGroup + "0011";
    public const string HeadsignTooLong = GtfsManagementErrorCodes.StopTimeGroup + "0012";
    public const string BookingRuleIdTooLong = GtfsManagementErrorCodes.StopTimeGroup + "0013";
    public const string LocationGroupOutOfFeed = GtfsManagementErrorCodes.StopTimeGroup + "0014";
    public const string StopWithLocationGroupForbidden = GtfsManagementErrorCodes.StopTimeGroup + "0015";
}
