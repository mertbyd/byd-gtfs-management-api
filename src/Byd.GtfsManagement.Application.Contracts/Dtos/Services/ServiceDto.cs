using System;

namespace Byd.GtfsManagement.Dtos.Services;

// islevi: GTFS service_id kaydini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
public class ServiceDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsServiceId { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
