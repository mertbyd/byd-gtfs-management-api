namespace Byd.GtfsManagement.Consts;

// islevi: stop_areas.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS durak bolge iliskisi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS durak bolge iliskisi sabitlerini tanimlar.</summary>
public static class StopAreaConsts
{
    public const string TableName = "StopAreas";
    public const string KeyIndex = "IX_StopAreas_FeedId_GtfsAreaId_GtfsStopId";
    public const int MaxGtfsAreaIdLength = 256;
    public const int MaxGtfsStopIdLength = 256;
}
