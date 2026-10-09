namespace Byd.GtfsManagement.Consts;

// islevi: Frequency filtre adlarini ve entity alanlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>Sefer sikligi filtre alanlarini tanimlar.</summary>
public static class FrequencyFilterFields
{
    public const string FeedId = "feedId";
    public const string TripId = "tripId";
    public const string StartTime = "startTime";
    public const string EndTime = "endTime";
    public const string HeadwaySecs = "headwaySecs";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string TripId = "TripId";
        public const string StartTime = "StartTime";
        public const string EndTime = "EndTime";
        public const string HeadwaySecs = "HeadwaySecs";
        public const string CreationTime = "CreationTime";
    }
}
