namespace Byd.GtfsManagement.Consts;

// islevi: rider_categories.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS yolcu kategorisi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS yolcu kategorisi sabitlerini tanimlar.</summary>
public static class RiderCategoryConsts
{
    public const string TableName = "RiderCategories";
    public const string KeyIndex = "IX_RiderCategories_FeedId_GtfsRiderCategoryId";
    public const int MaxGtfsRiderCategoryIdLength = 256;
    public const int MaxRiderCategoryNameLength = 256;
    public const int MaxEligibilityUrlLength = 2048;
}
