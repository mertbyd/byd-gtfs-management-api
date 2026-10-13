using System;

namespace Byd.GtfsManagement.Models.Attributions;

// islevi: Attribution olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS atif ortak domain girdi modelini tasir.</summary>
public abstract class AttributionInputModel
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
