using System;

namespace Byd.GtfsManagement.Dtos.Stops;

// islevi: stops.txt kaydinin istemciden gelen duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ortak alan sozlesmesidir.
public abstract class StopInputDto
{
    public int? LocationTypeId { get; set; }
    public int? WheelchairBoardingId { get; set; }
    public Guid? ParentStationId { get; set; }
    public Guid? LevelId { get; set; }
    public int? StopAccessId { get; set; }
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
}
