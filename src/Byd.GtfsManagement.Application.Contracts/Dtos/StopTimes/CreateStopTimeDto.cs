using System;

namespace Byd.GtfsManagement.Dtos.StopTimes;

// mirastan gelen: durak, sira, zaman ve binis alanlari.
// burada yazilan: degismeyen sefer bagi.
// islevi: StopTime olusturma istegini tasir.
// sistemdeki gorevi: Sefer kimligini ortak GTFS alanlarina ekler.
/// <summary>StopTime olusturma istegini tasir.</summary>
public sealed class CreateStopTimeDto : StopTimeInputDto
{
    public Guid TripId { get; set; }
}
