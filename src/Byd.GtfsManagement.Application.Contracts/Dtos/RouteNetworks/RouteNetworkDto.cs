using System;

namespace Byd.GtfsManagement.Dtos.RouteNetworks;

// islevi: RouteNetwork satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS hat ag iliskisi cevap alanlarini tasir.</summary>
public class RouteNetworkDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsNetworkId { get; set; } = default!;
    public string GtfsRouteId { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
