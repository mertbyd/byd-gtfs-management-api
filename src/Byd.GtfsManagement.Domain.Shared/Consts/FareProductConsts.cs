namespace Byd.GtfsManagement.Consts;

// islevi: fare_products.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ucret urunu sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ucret urunu sabitlerini tanimlar.</summary>
public static class FareProductConsts
{
    public const string TableName = "FareProducts";
    public const string KeyIndex = "IX_FareProducts_FeedId_GtfsFareProductId";
    public const int MaxGtfsFareProductIdLength = 256;
    public const int MaxFareProductNameLength = 256;
    public const int MaxGtfsRiderCategoryIdLength = 256;
    public const int MaxGtfsFareMediaIdLength = 256;
    public const int MaxCurrencyLength = 3;
}
