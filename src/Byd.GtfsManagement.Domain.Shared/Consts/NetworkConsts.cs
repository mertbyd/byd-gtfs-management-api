namespace Byd.GtfsManagement.Consts;

// islevi: networks.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ulasim agi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ulasim agi sabitlerini tanimlar.</summary>
public static class NetworkConsts
{
    public const string TableName = "Networks";
    public const string KeyIndex = "IX_Networks_FeedId_GtfsNetworkId";
    public const int MaxGtfsNetworkIdLength = 256;
    public const int MaxNetworkNameLength = 256;
}
