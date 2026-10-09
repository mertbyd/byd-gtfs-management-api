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
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Bagli ucret taniminin sistem kimligi.
    public Guid FareAttributeId { get; internal set; }

    // Bagli ucret tanimi kaydi.
    public FareAttribute FareAttribute { get; internal set; } = default!;

    // Istege bagli guzergah sistem kimligi.
    public Guid? RouteId { get; internal set; }

    // Bagli guzergah kaydi.
    public Route? Route { get; internal set; }

    // Baslangic bolgesi GTFS zone_id dis kimligi.
    public string? OriginId { get; internal set; }

    // Varis bolgesi GTFS zone_id dis kimligi.
    public string? DestinationId { get; internal set; }

    // Gecis bolgesi GTFS zone_id dis kimligi.
    public string? ContainsId { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected FareRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareRule(Guid id, Guid? tenantId, Guid feedId, Guid fareAttributeId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        FareAttributeId = fareAttributeId;
    }
}
