namespace Byd.GtfsManagement.Consts;

// islevi: areas.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS bolge sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS bolge sabitlerini tanimlar.</summary>
public static class AreaConsts
{
    public const string TableName = "Areas";
    public const string KeyIndex = "IX_Areas_FeedId_GtfsAreaId";
    public const int MaxGtfsAreaIdLength = 256;
    public const int MaxAreaNameLength = 256;
}
