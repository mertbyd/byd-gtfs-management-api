namespace Byd.GtfsManagement.Models.Frequencies;

// islevi: Siklik araliginin degistirilebilir GTFS alanlarini tasir.
// sistemdeki gorevi: Create ve Update Manager kurallarina ortak girdi verir.
/// <summary>Sefer sikligi domain girdisini tasir.</summary>
public abstract class FrequencyInputModel
{
    public string StartTime { get; set; } = default!;
    public string EndTime { get; set; } = default!;
    public int HeadwaySecs { get; set; }
    public int? ExactTimes { get; set; }
}
