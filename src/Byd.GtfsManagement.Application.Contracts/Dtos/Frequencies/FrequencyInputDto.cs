namespace Byd.GtfsManagement.Dtos.Frequencies;

// islevi: frequencies.txt duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerini ortak sozlesmeye baglar.
/// <summary>Sefer sikligi ortak istek alanlarini tasir.</summary>
public abstract class FrequencyInputDto
{
    public string StartTime { get; set; } = default!;
    public string EndTime { get; set; } = default!;
    public int HeadwaySecs { get; set; }
    public int? ExactTimes { get; set; }
}
