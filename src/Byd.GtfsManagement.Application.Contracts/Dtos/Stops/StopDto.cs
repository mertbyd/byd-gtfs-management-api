using System;

namespace Byd.GtfsManagement.Dtos.Stops;

// islevi: stops.txt kaydinin tum GTFS alanlarini ve bagli kayitlarin adlarini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId, GTFS kaydinin kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class StopDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public int? LocationTypeId { get; set; }
    public string? LocationTypeName { get; set; }
    public int? WheelchairBoardingId { get; set; }
    public string? WheelchairBoardingName { get; set; }
    public Guid? ParentStationId { get; set; }
    public string? ParentStationName { get; set; }
    public Guid? LevelId { get; set; }
    public string? LevelName { get; set; }
    public int? StopAccessId { get; set; }
    public string? StopAccessName { get; set; }
    public string GtfsStopId { get; set; } = default!;
    public string? ZoneId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? TtsName { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Url { get; set; }
    public string? Timezone { get; set; }
    public string? PlatformCode { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
