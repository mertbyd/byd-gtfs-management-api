namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: route_networks.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Hat ag iliskisi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS hat ag iliskisi hata kodlarini tanimlar.</summary>
public static class RouteNetworkExceptionCodes
{
    public const string RouteNetworkAlreadyExists = GtfsManagementErrorCodes.RouteNetworkGroup + "0002";
    public const string GtfsNetworkIdRequired = GtfsManagementErrorCodes.RouteNetworkGroup + "0003";
    public const string GtfsNetworkIdTooLong = GtfsManagementErrorCodes.RouteNetworkGroup + "0004";
    public const string GtfsRouteIdRequired = GtfsManagementErrorCodes.RouteNetworkGroup + "0005";
    public const string GtfsRouteIdTooLong = GtfsManagementErrorCodes.RouteNetworkGroup + "0006";
}
