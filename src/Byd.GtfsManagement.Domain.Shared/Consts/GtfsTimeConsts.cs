namespace Byd.GtfsManagement.Consts;

/// <summary>GTFS servis gunu saatlerinin ve tarihlerinin kararli bicim sozlesmesi.</summary>
// islevi: GTFS HH:MM:SS saat ve YYYYMMDD tarih bicimlerini tanimlar.
// sistemdeki gorevi: 24 saati asabilen servis gunu saatlerinin tum kullanimlarda ayni bicimde olmasini saglar.
public static class GtfsTimeConsts
{
    public const string FormatPattern = @"^[0-9]{2,}:[0-5][0-9]:[0-5][0-9]\z";
    public const string DateFormat = "yyyyMMdd";
}
