namespace Byd.GtfsManagement.Consts;

// islevi: FareAttribute filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ucret ozelligi filtre alanlarini tanimlar.</summary>
public static class FareAttributeFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsFareId = "gtfsFareId";
    public const string AgencyId = "agencyId";
    public const string Price = "price";
    public const string CurrencyType = "currencyType";
    public const string PaymentMethod = "paymentMethod";
    public const string Transfers = "transfers";
    public const string TransferDuration = "transferDuration";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsFareId = "GtfsFareId";
        public const string AgencyId = "AgencyId";
        public const string Price = "Price";
        public const string CurrencyType = "CurrencyType";
        public const string PaymentMethod = "PaymentMethod";
        public const string Transfers = "Transfers";
        public const string TransferDuration = "TransferDuration";
        public const string CreationTime = "CreationTime";
    }
}
