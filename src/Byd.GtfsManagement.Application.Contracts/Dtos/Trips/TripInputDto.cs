using System;

namespace Byd.GtfsManagement.Dtos.Trips;

// islevi: trips.txt kaydinin istemciden gelen duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ortak alan sozlesmesidir.
public abstract class TripInputDto
{
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public int? TripDirectionId { get; set; }
    public int? WheelchairAccessibleId { get; set; }
    public int? BikesAllowedId { get; set; }
    public int? CarsAllowedId { get; set; }
    public string GtfsTripId { get; set; } = default!;
    public string? GtfsShapeId { get; set; }
    public string? BlockId { get; set; }
    public string? Headsign { get; set; }
    public string? ShortName { get; set; }
}
