namespace Byd.GtfsManagement.Consts;

// islevi: stop_times.txt tablosunun adini ve trip_id + stop_sequence teklik indeksini tanimlar.
// sistemdeki gorevi: EF ve dogrulama katmanlarina kararli GTFS alan sinirlarini sunar.
/// <summary>Stop_times.txt tablo ve indeks adlarini tanimlar.</summary>
public static class StopTimeConsts
{
    public const string TableName = "StopTimes";
    public const string TripSequenceIndex = "IX_StopTimes_TripId_StopSequence";
    public const string StopIndex = "IX_StopTimes_StopId";
    public const int MaxHeadsignLength = 200;
    public const int MaxBookingRuleIdLength = 100;
}
