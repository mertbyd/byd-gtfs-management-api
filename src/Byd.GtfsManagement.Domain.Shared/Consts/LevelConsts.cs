namespace Byd.GtfsManagement.Consts;

// islevi: levels.txt tablo adi, indeks ve metin uzunluk sinirlarini tanimlar.
// sistemdeki gorevi: GTFS istasyon kati sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS kat sabitlerini tanimlar.</summary>
public static class LevelConsts
{
    public const string TableName = "Levels";
    public const string KeyIndex = "IX_Levels_FeedId_GtfsLevelId";
    public const int MaxGtfsLevelIdLength = 256;
    public const int MaxNameLength = 256;
}
