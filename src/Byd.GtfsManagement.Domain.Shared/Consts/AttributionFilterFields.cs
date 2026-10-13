namespace Byd.GtfsManagement.Consts;

// islevi: Attribution filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS atif filtre alanlarini tanimlar.</summary>
public static class AttributionFilterFields
{
    public const string FeedId = "feedId";
    public const string AgencyId = "agencyId";
    public const string RouteId = "routeId";
    public const string TripId = "tripId";
    public const string GtfsAttributionId = "gtfsAttributionId";
    public const string OrganizationName = "organizationName";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string AgencyId = "AgencyId";
        public const string RouteId = "RouteId";
        public const string TripId = "TripId";
        public const string GtfsAttributionId = "GtfsAttributionId";
        public const string OrganizationName = "OrganizationName";
        public const string CreationTime = "CreationTime";
    }
}
