namespace Byd.GtfsManagement.Dtos.Frequencies;

// mirastan gelen: baslangic, bitis, headway ve exact_times alanlari.
// burada yazilan: yok; sefer bagi degistirilemez.
// islevi: Frequency guncelleme istegini tasir.
// sistemdeki gorevi: Ortak GTFS alanlarini update akisine verir.
/// <summary>Sefer sikligi guncelleme istegini tasir.</summary>
public sealed class UpdateFrequencyDto : FrequencyInputDto
{
}
