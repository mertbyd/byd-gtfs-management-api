namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: trips.txt alan ve is kurali hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: GTFS sefer sozlesmesinin ihlallerini lokalize edilebilir kodlara baglar.
public static class TripExceptionCodes
{
    public const string RouteRequired = GtfsManagementErrorCodes.TripGroup + "0002";
    public const string ServiceRequired = GtfsManagementErrorCodes.TripGroup + "0003";
    public const string GtfsTripIdRequired = GtfsManagementErrorCodes.TripGroup + "0004";
    public const string GtfsTripIdTooLong = GtfsManagementErrorCodes.TripGroup + "0005";
    public const string HeadsignTooLong = GtfsManagementErrorCodes.TripGroup + "0006";
    public const string ShortNameTooLong = GtfsManagementErrorCodes.TripGroup + "0007";
    public const string BlockIdTooLong = GtfsManagementErrorCodes.TripGroup + "0008";
    public const string RouteOutOfFeed = GtfsManagementErrorCodes.TripGroup + "0009";
    public const string ServiceOutOfFeed = GtfsManagementErrorCodes.TripGroup + "0010";
    public const string GtfsShapeIdTooLong = GtfsManagementErrorCodes.TripGroup + "0011";
}
