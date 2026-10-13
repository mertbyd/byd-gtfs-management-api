namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: location_group_stops.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Konum grubu durak iliskisi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS konum grubu durak iliskisi hata kodlarini tanimlar.</summary>
public static class LocationGroupStopExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.LocationGroupStopGroup + "0001";
    public const string LocationGroupStopAlreadyExists = GtfsManagementErrorCodes.LocationGroupStopGroup + "0002";
    public const string GtfsLocationGroupIdRequired = GtfsManagementErrorCodes.LocationGroupStopGroup + "0003";
    public const string GtfsLocationGroupIdTooLong = GtfsManagementErrorCodes.LocationGroupStopGroup + "0004";
    public const string GtfsStopIdRequired = GtfsManagementErrorCodes.LocationGroupStopGroup + "0005";
    public const string GtfsStopIdTooLong = GtfsManagementErrorCodes.LocationGroupStopGroup + "0006";
}
