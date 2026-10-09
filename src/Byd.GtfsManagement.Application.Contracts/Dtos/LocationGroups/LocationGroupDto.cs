using System;

namespace Byd.GtfsManagement.Dtos.LocationGroups;

// islevi: LocationGroup satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS konum grubu cevap alanlarini tasir.</summary>
public class LocationGroupDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsLocationGroupId { get; set; } = default!;
    public string? LocationGroupName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
