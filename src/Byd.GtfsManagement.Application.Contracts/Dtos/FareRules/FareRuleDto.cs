using System;

namespace Byd.GtfsManagement.Dtos.FareRules;

// islevi: fare_rules.txt satirini, bagli ucret sinifi ve guzergahin adlariyla public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur. OriginId, DestinationId ve ContainsId GTFS zone_id
// degerleridir; GTFS'te ayri bir bolge tablosu olmadigi icin adlari yoktur.
public class FareRuleDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid FareAttributeId { get; set; }
    public string FareAttributeName { get; set; } = default!;
    public Guid? RouteId { get; set; }
    public string? RouteName { get; set; }
    public string? OriginId { get; set; }
    public string? DestinationId { get; set; }
    public string? ContainsId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
