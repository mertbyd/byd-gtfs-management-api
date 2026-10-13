namespace Byd.GtfsManagement.Consts;

// islevi: LocationGroupStop filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS konum grubu durak iliskisi filtre alanlarini tanimlar.</summary>
public static class LocationGroupStopFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsLocationGroupId = "gtfsLocationGroupId";
    public const string GtfsStopId = "gtfsStopId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsLocationGroupId = "GtfsLocationGroupId";
        public const string GtfsStopId = "GtfsStopId";
        public const string CreationTime = "CreationTime";
    }
}
