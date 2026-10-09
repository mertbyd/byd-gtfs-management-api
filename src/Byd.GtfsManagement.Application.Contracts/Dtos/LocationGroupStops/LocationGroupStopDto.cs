using System;

namespace Byd.GtfsManagement.Dtos.LocationGroupStops;

// islevi: LocationGroupStop satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS konum grubu durak iliskisi cevap alanlarini tasir.</summary>
public class LocationGroupStopDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsLocationGroupId { get; set; } = default!;
    public string GtfsStopId { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
