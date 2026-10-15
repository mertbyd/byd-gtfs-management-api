using System;

namespace Byd.GtfsManagement.Dtos.Networks;

// islevi: Network satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ulasim agi cevap alanlarini tasir.</summary>
public class NetworkDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsNetworkId { get; set; } = default!;
    public string? NetworkName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
