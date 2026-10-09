using System;

namespace Byd.GtfsManagement.Dtos.RouteNetworks;

// islevi: RouteNetwork satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS hat ag iliskisi cevap alanlarini tasir.</summary>
public class RouteNetworkDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsNetworkId { get; set; } = default!;
    public string GtfsRouteId { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
