using System;

namespace Byd.GtfsManagement.Dtos.FareRules;

// islevi: fare_rules.txt duzenlenebilir kural alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ucret kurali ortak istek alanlarini tasir.</summary>
public abstract class FareRuleInputDto
{
    public Guid FareAttributeId { get; set; }
    public Guid? RouteId { get; set; }
    public string? OriginId { get; set; }
    public string? DestinationId { get; set; }
    public string? ContainsId { get; set; }
}
