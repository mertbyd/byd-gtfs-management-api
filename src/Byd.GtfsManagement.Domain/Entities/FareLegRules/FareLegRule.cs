using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareLegRules;

// islevi: fare_leg_rules.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ucret etap kurali verilerini Feed altinda tutar.
/// <summary>GTFS ucret etap kurali kaydini temsil eder.</summary>
public class FareLegRule : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Etap grubu dis kimligi.
    public string? LegGroupId { get; internal set; }

    // Ulasim agi dis kimligi.
    public string? NetworkId { get; internal set; }

    // Kalkis bolgesi dis kimligi.
    public string? FromAreaId { get; internal set; }

    // Varis bolgesi dis kimligi.
    public string? ToAreaId { get; internal set; }

    // Kalkis zaman araligi grubu dis kimligi.
    public string? FromTimeframeGroupId { get; internal set; }

    // Varis zaman araligi grubu dis kimligi.
    public string? ToTimeframeGroupId { get; internal set; }

    // GTFS fare_product_id dis kimligi.
    public string GtfsFareProductId { get; internal set; } = default!;

    // Kural oncelik degeri.
    public int? RulePriority { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected FareLegRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareLegRule(Guid id, Guid? tenantId, Guid feedId, string gtfsFareProductId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsFareProductId = gtfsFareProductId;
    }
}
