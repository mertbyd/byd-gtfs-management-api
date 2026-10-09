namespace Byd.GtfsManagement.Consts;

// islevi: Shape filtre adlarini ve entity alanlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS sekil filtre alanlarini tanimlar.</summary>
public static class ShapeFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsShapeId = "gtfsShapeId";
    public const string Sequence = "sequence";
    public const string Latitude = "latitude";
    public const string Longitude = "longitude";
    public const string DistTraveled = "distTraveled";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsShapeId = "GtfsShapeId";
        public const string Sequence = "Sequence";
        public const string Latitude = "Latitude";
        public const string Longitude = "Longitude";
        public const string DistTraveled = "DistTraveled";
        public const string CreationTime = "CreationTime";
    }
}
