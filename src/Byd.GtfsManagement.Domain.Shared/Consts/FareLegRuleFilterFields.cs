namespace Byd.GtfsManagement.Consts;

// islevi: FareLegRule filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ucret etap kurali filtre alanlarini tanimlar.</summary>
public static class FareLegRuleFilterFields
{
    public const string FeedId = "feedId";
    public const string LegGroupId = "legGroupId";
    public const string NetworkId = "networkId";
    public const string FromAreaId = "fromAreaId";
    public const string ToAreaId = "toAreaId";
    public const string FromTimeframeGroupId = "fromTimeframeGroupId";
    public const string ToTimeframeGroupId = "toTimeframeGroupId";
    public const string GtfsFareProductId = "gtfsFareProductId";
    public const string RulePriority = "rulePriority";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string LegGroupId = "LegGroupId";
        public const string NetworkId = "NetworkId";
        public const string FromAreaId = "FromAreaId";
        public const string ToAreaId = "ToAreaId";
        public const string FromTimeframeGroupId = "FromTimeframeGroupId";
        public const string ToTimeframeGroupId = "ToTimeframeGroupId";
        public const string GtfsFareProductId = "GtfsFareProductId";
        public const string RulePriority = "RulePriority";
        public const string CreationTime = "CreationTime";
    }
}
