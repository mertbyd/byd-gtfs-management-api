using System;

namespace Byd.GtfsManagement.Dtos.Attributions;

// islevi: attributions.txt duzenlenebilir atif alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS atif ortak istek alanlarini tasir.</summary>
public abstract class AttributionInputDto
{
    public Guid? AgencyId { get; set; }
    public Guid? RouteId { get; set; }
    public Guid? TripId { get; set; }
    public string? GtfsAttributionId { get; set; }
    public string OrganizationName { get; set; } = default!;
    public int? IsProducer { get; set; }
    public int? IsOperator { get; set; }
    public int? IsAuthority { get; set; }
    public string? AttributionUrl { get; set; }
    public string? AttributionEmail { get; set; }
    public string? AttributionPhone { get; set; }
}
