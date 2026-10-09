using System;

namespace Byd.GtfsManagement.Models.Frequencies;

// mirastan gelen: baslangic, bitis, headway ve exact_times alanlari.
// burada yazilan: degismeyen sefer bagi.
// islevi: Yeni siklik kaydinin sefer kimligini tasir.
// sistemdeki gorevi: Manager'in kapsam ve bag kurallarini baslatir.
/// <summary>Sefer sikligi olusturma modelini tasir.</summary>
public sealed class FrequencyCreateModel : FrequencyInputModel
{
    public Guid TripId { get; set; }
}
