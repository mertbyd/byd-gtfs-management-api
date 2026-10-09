namespace Byd.GtfsManagement.Consts;

// islevi: LocationGroup filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS konum grubu filtre alanlarini tanimlar.</summary>
public static class LocationGroupFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsLocationGroupId = "gtfsLocationGroupId";
    public const string LocationGroupName = "locationGroupName";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsLocationGroupId = "GtfsLocationGroupId";
        public const string LocationGroupName = "LocationGroupName";
        public const string CreationTime = "CreationTime";
    }
}
