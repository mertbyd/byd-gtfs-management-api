using System;

namespace Byd.GtfsManagement.Dtos.Agencies;

// islevi: agency.txt kaydinin tum GTFS alanlarini ve lookup adini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId, GTFS kaydinin kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class AgencyDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public int? CemvSupportId { get; set; }
    public string? CemvSupportName { get; set; }
    public string? GtfsAgencyId { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Timezone { get; set; } = default!;
    public string? Language { get; set; }
    public string? Phone { get; set; }
    public string? FareUrl { get; set; }
    public string? Email { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
