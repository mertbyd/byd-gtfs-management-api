using System;

namespace Byd.GtfsManagement.Models.StopTimes;

// mirastan gelen: durak, sira ve zaman alanlari.
// burada yazilan: degismeyen sefer bagi.
// islevi: StopTime olusturma alanlarini tasir.
// sistemdeki gorevi: Degismeyen sefer bagini ortak girdiye ekler.
/// <summary>StopTime olusturma alanlarini tasir.</summary>
public sealed class StopTimeCreateModel : StopTimeInputModel
{
    public Guid TripId { get; set; }
}
