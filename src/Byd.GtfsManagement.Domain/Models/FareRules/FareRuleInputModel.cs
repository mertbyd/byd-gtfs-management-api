using System;

namespace Byd.GtfsManagement.Models.FareRules;

// islevi: FareRule olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ucret kurali ortak domain girdi modelini tasir.</summary>
public abstract class FareRuleInputModel
{
    public Guid FareAttributeId { get; set; }
    public Guid? RouteId { get; set; }
    public string? OriginId { get; set; }
    public string? DestinationId { get; set; }
    public string? ContainsId { get; set; }
}
