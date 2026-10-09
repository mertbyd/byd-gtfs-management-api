namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: routes.txt alan ve is kurali hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: GTFS guzergah sozlesmesinin ihlallerini lokalize edilebilir kodlara baglar.
public static class RouteExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.RouteGroup + "0001";
    public const string RouteTypeRequired = GtfsManagementErrorCodes.RouteGroup + "0002";
    public const string GtfsRouteIdRequired = GtfsManagementErrorCodes.RouteGroup + "0003";
    public const string GtfsRouteIdTooLong = GtfsManagementErrorCodes.RouteGroup + "0004";
    public const string NameRequired = GtfsManagementErrorCodes.RouteGroup + "0005";
    public const string ShortNameTooLong = GtfsManagementErrorCodes.RouteGroup + "0006";
    public const string LongNameTooLong = GtfsManagementErrorCodes.RouteGroup + "0007";
    public const string DescriptionTooLong = GtfsManagementErrorCodes.RouteGroup + "0008";
    public const string UrlTooLong = GtfsManagementErrorCodes.RouteGroup + "0009";
    public const string ColorInvalid = GtfsManagementErrorCodes.RouteGroup + "0010";
    public const string TextColorInvalid = GtfsManagementErrorCodes.RouteGroup + "0011";
    public const string SortOrderNegative = GtfsManagementErrorCodes.RouteGroup + "0012";
    public const string AgencyRequired = GtfsManagementErrorCodes.RouteGroup + "0013";
    public const string AgencyOutOfFeed = GtfsManagementErrorCodes.RouteGroup + "0014";
    public const string NetworkOutOfFeed = GtfsManagementErrorCodes.RouteGroup + "0015";
}
