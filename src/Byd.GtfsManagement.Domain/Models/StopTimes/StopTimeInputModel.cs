using System;

namespace Byd.GtfsManagement.Models.StopTimes;

// islevi: stop_times.txt satirinin duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncelleme ayni Manager kurallarini kullanir.
/// <summary>StopTime duzenlenebilir domain alanlarini tasir.</summary>
public abstract class StopTimeInputModel
{
    public Guid? StopId { get; set; }
    public Guid? LocationGroupId { get; set; }
    public int? PickupTypeId { get; set; }
    public int? DropOffTypeId { get; set; }
    public int? ContinuousPickupId { get; set; }
    public int? ContinuousDropOffId { get; set; }
    public string? PickupBookingRuleId { get; set; }
    public string? DropOffBookingRuleId { get; set; }
    public int StopSequence { get; set; }
    public string? ArrivalTime { get; set; }
    public string? DepartureTime { get; set; }
    public string? StopHeadsign { get; set; }
    public string? StartPickupDropOffWindow { get; set; }
    public string? EndPickupDropOffWindow { get; set; }
    public double? ShapeDistTraveled { get; set; }
    public int? Timepoint { get; set; }
}
