namespace Byd.GtfsManagement.Consts;

// islevi: Trip filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class TripFilterFields
{
    public const string FeedId = "feedId";
    public const string RouteId = "routeId";
    public const string ServiceId = "serviceId";
    public const string TripDirectionId = "tripDirectionId";
    public const string GtfsTripId = "gtfsTripId";
    public const string Headsign = "headsign";
    public const string BlockId = "blockId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string RouteId = "RouteId";
        public const string ServiceId = "ServiceId";
        public const string TripDirectionId = "TripDirectionId";
        public const string GtfsTripId = "GtfsTripId";
        public const string Headsign = "Headsign";
        public const string BlockId = "BlockId";
        public const string CreationTime = "CreationTime";
    }
}
