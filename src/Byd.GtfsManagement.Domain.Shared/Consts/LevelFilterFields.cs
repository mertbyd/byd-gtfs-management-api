namespace Byd.GtfsManagement.Consts;

// islevi: Level filtre adlarini ve entity alanlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS kat filtre alanlarini tanimlar.</summary>
public static class LevelFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsLevelId = "gtfsLevelId";
    public const string LevelIndex = "levelIndex";
    public const string Name = "name";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsLevelId = "GtfsLevelId";
        public const string LevelIndex = "LevelIndex";
        public const string Name = "Name";
        public const string CreationTime = "CreationTime";
    }
}
