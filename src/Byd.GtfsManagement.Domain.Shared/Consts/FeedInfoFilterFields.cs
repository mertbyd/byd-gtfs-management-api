namespace Byd.GtfsManagement.Consts;

// islevi: FeedInfo filtre adlarini ve entity alanlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS yayimci bilgisi filtre alanlarini tanimlar.</summary>
public static class FeedInfoFilterFields
{
    public const string FeedId = "feedId";
    public const string PublisherName = "publisherName";
    public const string Language = "language";
    public const string DefaultLanguage = "defaultLanguage";
    public const string StartDate = "startDate";
    public const string EndDate = "endDate";
    public const string Version = "version";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string PublisherName = "PublisherName";
        public const string Language = "Language";
        public const string DefaultLanguage = "DefaultLanguage";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string Version = "Version";
        public const string CreationTime = "CreationTime";
    }
}
