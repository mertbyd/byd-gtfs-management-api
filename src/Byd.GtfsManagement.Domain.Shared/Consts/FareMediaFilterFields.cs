namespace Byd.GtfsManagement.Consts;

// islevi: FareMedia filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ucret medyasi filtre alanlarini tanimlar.</summary>
public static class FareMediaFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsFareMediaId = "gtfsFareMediaId";
    public const string FareMediaName = "fareMediaName";
    public const string FareMediaType = "fareMediaType";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsFareMediaId = "GtfsFareMediaId";
        public const string FareMediaName = "FareMediaName";
        public const string FareMediaType = "FareMediaType";
        public const string CreationTime = "CreationTime";
    }
}
