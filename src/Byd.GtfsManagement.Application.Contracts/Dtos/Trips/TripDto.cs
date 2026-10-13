using System;

namespace Byd.GtfsManagement.Dtos.Trips;

// islevi: trips.txt kaydinin tum GTFS alanlarini ve bagli kayitlarin adlarini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId, GTFS kaydinin kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class TripDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid RouteId { get; set; }
    public string RouteName { get; set; } = default!;
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = default!;
    public int? TripDirectionId { get; set; }
    public string? TripDirectionName { get; set; }
    public int? WheelchairAccessibleId { get; set; }
    public string? WheelchairAccessibleName { get; set; }
    public int? BikesAllowedId { get; set; }
    public string? BikesAllowedName { get; set; }
    public int? CarsAllowedId { get; set; }
    public string? CarsAllowedName { get; set; }
    public string GtfsTripId { get; set; } = default!;
    public string? GtfsShapeId { get; set; }
    public string? BlockId { get; set; }
    public string? Headsign { get; set; }
    public string? ShortName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
