namespace Byd.GtfsManagement.Consts;

// islevi: frequencies.txt tablo, indeks ve exact_times kodlarini tanimlar.
// sistemdeki gorevi: GTFS siklik sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>Sefer sikligi sabitlerini tanimlar.</summary>
public static class FrequencyConsts
{
    public const string TableName = "Frequencies";
    public const string TripStartIndex = "IX_Frequencies_TripId_StartTime";
    public const int FrequencyBasedCode = 0;
    public const int ScheduleBasedCode = 1;
}
