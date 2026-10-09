namespace Byd.GtfsManagement.Consts;

// islevi: StopArea filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS durak bolge iliskisi filtre alanlarini tanimlar.</summary>
public static class StopAreaFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsAreaId = "gtfsAreaId";
    public const string GtfsStopId = "gtfsStopId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsAreaId = "GtfsAreaId";
        public const string GtfsStopId = "GtfsStopId";
        public const string CreationTime = "CreationTime";
    }
}
