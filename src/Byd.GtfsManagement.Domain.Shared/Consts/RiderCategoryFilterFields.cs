namespace Byd.GtfsManagement.Consts;

// islevi: RiderCategory filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS yolcu kategorisi filtre alanlarini tanimlar.</summary>
public static class RiderCategoryFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsRiderCategoryId = "gtfsRiderCategoryId";
    public const string RiderCategoryName = "riderCategoryName";
    public const string IsDefaultFareCategory = "isDefaultFareCategory";
    public const string EligibilityUrl = "eligibilityUrl";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsRiderCategoryId = "GtfsRiderCategoryId";
        public const string RiderCategoryName = "RiderCategoryName";
        public const string IsDefaultFareCategory = "IsDefaultFareCategory";
        public const string EligibilityUrl = "EligibilityUrl";
        public const string CreationTime = "CreationTime";
    }
}
