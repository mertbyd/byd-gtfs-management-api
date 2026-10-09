namespace Byd.GtfsManagement.Consts;

// islevi: Route filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class RouteFilterFields
{
    public const string FeedId = "feedId";
    public const string AgencyId = "agencyId";
    public const string RouteTypeId = "routeTypeId";
    public const string GtfsRouteId = "gtfsRouteId";
    public const string ShortName = "shortName";
    public const string LongName = "longName";
    public const string SortOrder = "sortOrder";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string AgencyId = "AgencyId";
        public const string RouteTypeId = "RouteTypeId";
        public const string GtfsRouteId = "GtfsRouteId";
        public const string ShortName = "ShortName";
        public const string LongName = "LongName";
        public const string SortOrder = "SortOrder";
        public const string CreationTime = "CreationTime";
    }
}
