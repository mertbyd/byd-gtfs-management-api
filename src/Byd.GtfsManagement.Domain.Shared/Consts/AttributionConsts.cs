namespace Byd.GtfsManagement.Consts;

// islevi: attributions.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS atif sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS atif sabitlerini tanimlar.</summary>
public static class AttributionConsts
{
    public const string TableName = "Attributions";
    public const string KeyIndex = "IX_Attributions_FeedId_GtfsAttributionId";
    public const int MaxGtfsAttributionIdLength = 256;
    public const int MaxOrganizationNameLength = 256;
    public const int MaxAttributionUrlLength = 2048;
    public const int MaxAttributionEmailLength = 256;
    public const int MaxAttributionPhoneLength = 64;
}
