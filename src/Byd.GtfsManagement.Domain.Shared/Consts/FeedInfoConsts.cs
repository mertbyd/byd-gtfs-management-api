namespace Byd.GtfsManagement.Consts;

// islevi: feed_info.txt tablo adi, indeks ve metin uzunluk sinirlarini tanimlar.
// sistemdeki gorevi: GTFS veri seti yayimci sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS yayimci bilgisi sabitlerini tanimlar.</summary>
public static class FeedInfoConsts
{
    public const string TableName = "FeedInfos";
    public const string KeyIndex = "IX_FeedInfos_FeedId";
    public const int MaxPublisherNameLength = 256;
    public const int MaxUrlLength = 2048;
    public const int MaxLanguageLength = 20;
    public const int MaxEmailLength = 256;
    public const int MaxVersionLength = 256;
}
