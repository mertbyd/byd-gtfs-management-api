namespace Byd.GtfsManagement.Consts;

// islevi: FareTransferRule filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ucret aktarma kurali filtre alanlarini tanimlar.</summary>
public static class FareTransferRuleFilterFields
{
    public const string FeedId = "feedId";
    public const string FromLegGroupId = "fromLegGroupId";
    public const string ToLegGroupId = "toLegGroupId";
    public const string TransferCount = "transferCount";
    public const string DurationLimit = "durationLimit";
    public const string DurationLimitType = "durationLimitType";
    public const string FareTransferType = "fareTransferType";
    public const string GtfsFareProductId = "gtfsFareProductId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string FromLegGroupId = "FromLegGroupId";
        public const string ToLegGroupId = "ToLegGroupId";
        public const string TransferCount = "TransferCount";
        public const string DurationLimit = "DurationLimit";
        public const string DurationLimitType = "DurationLimitType";
        public const string FareTransferType = "FareTransferType";
        public const string GtfsFareProductId = "GtfsFareProductId";
        public const string CreationTime = "CreationTime";
    }
}
