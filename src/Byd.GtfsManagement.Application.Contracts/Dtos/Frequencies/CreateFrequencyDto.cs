using System;

namespace Byd.GtfsManagement.Dtos.Frequencies;

// mirastan gelen: baslangic, bitis, headway ve exact_times alanlari.
// burada yazilan: degismeyen sefer bagi.
// islevi: Frequency olusturma istegini tasir.
// sistemdeki gorevi: Sefer kimligini ortak GTFS alanlarina ekler.
/// <summary>Sefer sikligi olusturma istegini tasir.</summary>
public sealed class CreateFrequencyDto : FrequencyInputDto
{
    public Guid TripId { get; set; }
}
