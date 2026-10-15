using System;

namespace Byd.GtfsManagement.Dtos.FareRules;

// islevi: fare_rules.txt satirini, bagli ucret sinifi ve guzergahin adlariyla public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur. OriginId, DestinationId ve ContainsId GTFS zone_id
// degerleridir; GTFS'te ayri bir bolge tablosu olmadigi icin adlari yoktur.
public class FareRuleDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }
    public Guid FareAttributeId { get; set; }
    public Guid? RouteId { get; set; }
    public string? OriginId { get; set; }
    public string? DestinationId { get; set; }
    public string? ContainsId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string FareAttributeName { get; set; } = default!;
    public string? RouteName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
