namespace Byd.GtfsManagement.Consts;

// islevi: fare_media.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ucret medyasi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ucret medyasi sabitlerini tanimlar.</summary>
public static class FareMediaConsts
{
    public const string TableName = "FareMedia";
    public const string KeyIndex = "IX_FareMedia_FeedId_GtfsFareMediaId";
    public const int MaxGtfsFareMediaIdLength = 256;
    public const int MaxFareMediaNameLength = 256;
}
