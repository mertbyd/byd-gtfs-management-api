using System;

namespace Byd.GtfsManagement.Dtos.Networks;

// islevi: Network satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ulasim agi cevap alanlarini tasir.</summary>
public class NetworkDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsNetworkId { get; set; } = default!;
    public string? NetworkName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
