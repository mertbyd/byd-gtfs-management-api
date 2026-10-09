namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: stops.txt alan ve is kurali hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: GTFS durak sozlesmesinin ihlallerini lokalize edilebilir kodlara baglar.
public static class StopExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.StopGroup + "0001";
    public const string GtfsStopIdRequired = GtfsManagementErrorCodes.StopGroup + "0002";
    public const string GtfsStopIdTooLong = GtfsManagementErrorCodes.StopGroup + "0003";
    public const string CodeTooLong = GtfsManagementErrorCodes.StopGroup + "0004";
    public const string NameRequired = GtfsManagementErrorCodes.StopGroup + "0005";
    public const string NameTooLong = GtfsManagementErrorCodes.StopGroup + "0006";
    public const string TtsNameTooLong = GtfsManagementErrorCodes.StopGroup + "0007";
    public const string DescriptionTooLong = GtfsManagementErrorCodes.StopGroup + "0008";
    public const string CoordinatesRequired = GtfsManagementErrorCodes.StopGroup + "0009";
    public const string LatitudeOutOfRange = GtfsManagementErrorCodes.StopGroup + "0010";
    public const string LongitudeOutOfRange = GtfsManagementErrorCodes.StopGroup + "0011";
    public const string ZoneIdTooLong = GtfsManagementErrorCodes.StopGroup + "0012";
    public const string UrlTooLong = GtfsManagementErrorCodes.StopGroup + "0013";
    public const string TimezoneTooLong = GtfsManagementErrorCodes.StopGroup + "0014";
    public const string TimezoneInvalid = GtfsManagementErrorCodes.StopGroup + "0015";
    public const string PlatformCodeTooLong = GtfsManagementErrorCodes.StopGroup + "0016";
    public const string ParentStationRequired = GtfsManagementErrorCodes.StopGroup + "0017";
    public const string ParentStationNotAllowed = GtfsManagementErrorCodes.StopGroup + "0018";
    public const string ParentStationNotAStation = GtfsManagementErrorCodes.StopGroup + "0019";
    public const string ParentStationOutOfFeed = GtfsManagementErrorCodes.StopGroup + "0020";
    public const string LevelOutOfFeed = GtfsManagementErrorCodes.StopGroup + "0021";
    public const string StopAccessNotAllowed = GtfsManagementErrorCodes.StopGroup + "0022";
}
