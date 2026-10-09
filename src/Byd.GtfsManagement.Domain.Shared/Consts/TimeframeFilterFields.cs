namespace Byd.GtfsManagement.Consts;

// islevi: Timeframe filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS zaman araligi filtre alanlarini tanimlar.</summary>
public static class TimeframeFilterFields
{
    public const string FeedId = "feedId";
    public const string TimeframeGroupId = "timeframeGroupId";
    public const string StartTime = "startTime";
    public const string EndTime = "endTime";
    public const string GtfsServiceId = "gtfsServiceId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string TimeframeGroupId = "TimeframeGroupId";
        public const string StartTime = "StartTime";
        public const string EndTime = "EndTime";
        public const string GtfsServiceId = "GtfsServiceId";
        public const string CreationTime = "CreationTime";
    }
}
