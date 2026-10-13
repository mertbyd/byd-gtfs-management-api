using System;

namespace Byd.GtfsManagement.Dtos.Services;

// islevi: GTFS service_id kaydini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId, GTFS kaydinin kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class ServiceDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsServiceId { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
