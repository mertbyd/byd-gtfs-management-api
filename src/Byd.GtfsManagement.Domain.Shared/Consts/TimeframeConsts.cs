namespace Byd.GtfsManagement.Consts;

// islevi: timeframes.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS zaman araligi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS zaman araligi sabitlerini tanimlar.</summary>
public static class TimeframeConsts
{
    public const string TableName = "Timeframes";
    public const string KeyIndex = "IX_Timeframes_FeedId_TimeframeGroupId_GtfsServiceId";
    public const int MaxTimeframeGroupIdLength = 256;
    public const int MaxStartTimeLength = 10;
    public const int MaxEndTimeLength = 10;
    public const int MaxGtfsServiceIdLength = 256;
}
