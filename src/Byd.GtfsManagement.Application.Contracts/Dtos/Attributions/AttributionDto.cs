using System;

namespace Byd.GtfsManagement.Dtos.Attributions;

// islevi: attributions.txt satirini, bagli ajans, guzergah ve seferin adlariyla public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class AttributionDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid? AgencyId { get; set; }
    public string? AgencyName { get; set; }
    public Guid? RouteId { get; set; }
    public string? RouteName { get; set; }
    public Guid? TripId { get; set; }
    public string? TripName { get; set; }
    public string? GtfsAttributionId { get; set; }
    public string OrganizationName { get; set; } = default!;
    public int? IsProducer { get; set; }
    public int? IsOperator { get; set; }
    public int? IsAuthority { get; set; }
    public string? AttributionUrl { get; set; }
    public string? AttributionEmail { get; set; }
    public string? AttributionPhone { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
