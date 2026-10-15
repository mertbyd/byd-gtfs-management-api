using System;
using Byd.GtfsManagement.Entities.FareAttributes;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareRules;

// islevi: fare_rules.txt ucret sinifinin guzergah ve bolge eslemelerini saklar.
// sistemdeki gorevi: Bilet fiyatlarinin hangi guzergah ve bolgelerde gecerli oldugunu tutar.
/// <summary>GTFS ucret kuralini temsil eder.</summary>
public class FareRule : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Bagli ucret taniminin sistem kimligi.
    public Guid FareAttributeId { get; protected internal set; }

    // Istege bagli guzergah sistem kimligi.
    public Guid? RouteId { get; protected internal set; }

    // Baslangic bolgesi GTFS zone_id dis kimligi.
    public string? OriginId { get; protected internal set; }

    // Varis bolgesi GTFS zone_id dis kimligi.
    public string? DestinationId { get; protected internal set; }

    // Gecis bolgesi GTFS zone_id dis kimligi.
    public string? ContainsId { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Bagli ucret tanimi kaydi.
    public FareAttribute FareAttribute { get; protected internal set; } = default!;
    // Bagli guzergah kaydi.
    public Route? Route { get; protected internal set; }

    // EF materialization icin bos kabuk kurar.
    protected FareRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareRule(Guid id, Guid feedId, Guid fareAttributeId) : base(id)
    {
        FeedId = feedId;
        FareAttributeId = fareAttributeId;
    }
}
