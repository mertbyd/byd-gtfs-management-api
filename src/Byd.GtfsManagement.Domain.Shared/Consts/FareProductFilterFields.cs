namespace Byd.GtfsManagement.Consts;

// islevi: FareProduct filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ucret urunu filtre alanlarini tanimlar.</summary>
public static class FareProductFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsFareProductId = "gtfsFareProductId";
    public const string FareProductName = "fareProductName";
    public const string GtfsRiderCategoryId = "gtfsRiderCategoryId";
    public const string GtfsFareMediaId = "gtfsFareMediaId";
    public const string Amount = "amount";
    public const string Currency = "currency";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsFareProductId = "GtfsFareProductId";
        public const string FareProductName = "FareProductName";
        public const string GtfsRiderCategoryId = "GtfsRiderCategoryId";
        public const string GtfsFareMediaId = "GtfsFareMediaId";
        public const string Amount = "Amount";
        public const string Currency = "Currency";
        public const string CreationTime = "CreationTime";
    }
}
