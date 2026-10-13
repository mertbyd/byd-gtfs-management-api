using System;

namespace Byd.GtfsManagement.Dtos.FareMedias;

// islevi: FareMedia satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ucret medyasi cevap alanlarini tasir.</summary>
public class FareMediaDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsFareMediaId { get; set; } = default!;
    public string? FareMediaName { get; set; }
    public int FareMediaType { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
