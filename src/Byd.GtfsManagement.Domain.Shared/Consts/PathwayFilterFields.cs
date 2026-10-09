namespace Byd.GtfsManagement.Consts;

// islevi: Pathway filtre adlarini ve entity alanlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS yuruyus yolu filtre alanlarini tanimlar.</summary>
public static class PathwayFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsPathwayId = "gtfsPathwayId";
    public const string FromStopId = "fromStopId";
    public const string ToStopId = "toStopId";
    public const string PathwayMode = "pathwayMode";
    public const string IsBidirectional = "isBidirectional";
    public const string Length = "length";
    public const string TraversalTime = "traversalTime";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsPathwayId = "GtfsPathwayId";
        public const string FromStopId = "FromStopId";
        public const string ToStopId = "ToStopId";
        public const string PathwayMode = "PathwayMode";
        public const string IsBidirectional = "IsBidirectional";
        public const string Length = "Length";
        public const string TraversalTime = "TraversalTime";
        public const string CreationTime = "CreationTime";
    }
}
