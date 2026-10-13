namespace Byd.GtfsManagement.Consts;

// islevi: RouteNetwork filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS hat ag iliskisi filtre alanlarini tanimlar.</summary>
public static class RouteNetworkFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsNetworkId = "gtfsNetworkId";
    public const string GtfsRouteId = "gtfsRouteId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsNetworkId = "GtfsNetworkId";
        public const string GtfsRouteId = "GtfsRouteId";
        public const string CreationTime = "CreationTime";
    }
}
