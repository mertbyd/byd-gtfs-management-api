using System;

namespace Byd.GtfsManagement.Dtos.LocationGroups;

// islevi: LocationGroup satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS konum grubu cevap alanlarini tasir.</summary>
public class LocationGroupDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsLocationGroupId { get; set; } = default!;
    public string? LocationGroupName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
