using System;

namespace Byd.GtfsManagement.Dtos.Trips;

// islevi: trips.txt kaydinin tum GTFS alanlarini ve bagli kayitlarin adlarini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
public class TripDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public int? TripDirectionId { get; set; }
    public int? WheelchairAccessibleId { get; set; }
    public int? BikesAllowedId { get; set; }
    public int? CarsAllowedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string RouteName { get; set; } = default!;
    public string ServiceName { get; set; } = default!;
    public string? TripDirectionName { get; set; }
    public string? WheelchairAccessibleName { get; set; }
    public string? BikesAllowedName { get; set; }
    public string? CarsAllowedName { get; set; }
    public string GtfsTripId { get; set; } = default!;
    public string? GtfsShapeId { get; set; }
    public string? BlockId { get; set; }
    public string? Headsign { get; set; }
    public string? ShortName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
