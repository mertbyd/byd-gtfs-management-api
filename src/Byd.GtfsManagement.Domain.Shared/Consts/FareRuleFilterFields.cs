namespace Byd.GtfsManagement.Consts;

// islevi: FareRule filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ucret kurali filtre alanlarini tanimlar.</summary>
public static class FareRuleFilterFields
{
    public const string FeedId = "feedId";
    public const string FareAttributeId = "fareAttributeId";
    public const string RouteId = "routeId";
    public const string OriginId = "originId";
    public const string DestinationId = "destinationId";
    public const string ContainsId = "containsId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string FareAttributeId = "FareAttributeId";
        public const string RouteId = "RouteId";
        public const string OriginId = "OriginId";
        public const string DestinationId = "DestinationId";
        public const string ContainsId = "ContainsId";
        public const string CreationTime = "CreationTime";
    }
}
