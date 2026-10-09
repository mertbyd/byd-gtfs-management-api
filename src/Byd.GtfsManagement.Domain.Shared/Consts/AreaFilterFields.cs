namespace Byd.GtfsManagement.Consts;

// islevi: Area filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS bolge filtre alanlarini tanimlar.</summary>
public static class AreaFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsAreaId = "gtfsAreaId";
    public const string AreaName = "areaName";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsAreaId = "GtfsAreaId";
        public const string AreaName = "AreaName";
        public const string CreationTime = "CreationTime";
    }
}
