namespace Byd.GtfsManagement.Consts;

// islevi: FareLegJoinRule filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ucret etap birlestirme kurali filtre alanlarini tanimlar.</summary>
public static class FareLegJoinRuleFilterFields
{
    public const string FeedId = "feedId";
    public const string FromNetworkId = "fromNetworkId";
    public const string ToNetworkId = "toNetworkId";
    public const string FromStopId = "fromStopId";
    public const string ToStopId = "toStopId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string FromNetworkId = "FromNetworkId";
        public const string ToNetworkId = "ToNetworkId";
        public const string FromStopId = "FromStopId";
        public const string ToStopId = "ToStopId";
        public const string CreationTime = "CreationTime";
    }
}
