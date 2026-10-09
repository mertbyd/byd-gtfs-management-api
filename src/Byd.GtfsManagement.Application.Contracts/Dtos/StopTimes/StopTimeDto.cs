using System;

namespace Byd.GtfsManagement.Dtos.StopTimes;

// islevi: stop_times.txt satirini, bagli sefer, durak ve lookup kayitlarinin adlariyla tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur. Rezervasyon kurali alanlari GTFS booking_rule_id
// degerleridir.
public class StopTimeDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid TripId { get; set; }
    public string TripName { get; set; } = default!;
    public Guid? StopId { get; set; }
    public string? StopName { get; set; }
    public Guid? LocationGroupId { get; set; }
    public string? LocationGroupName { get; set; }
    public int? PickupTypeId { get; set; }
    public string? PickupTypeName { get; set; }
    public int? DropOffTypeId { get; set; }
    public string? DropOffTypeName { get; set; }
    public int? ContinuousPickupId { get; set; }
    public string? ContinuousPickupName { get; set; }
    public int? ContinuousDropOffId { get; set; }
    public string? ContinuousDropOffName { get; set; }
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
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
