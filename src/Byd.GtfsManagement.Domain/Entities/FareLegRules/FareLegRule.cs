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
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Etap grubu dis kimligi.
    public string? LegGroupId { get; protected internal set; }

    // Ulasim agi dis kimligi.
    public string? NetworkId { get; protected internal set; }

    // Kalkis bolgesi dis kimligi.
    public string? FromAreaId { get; protected internal set; }

    // Varis bolgesi dis kimligi.
    public string? ToAreaId { get; protected internal set; }

    // Kalkis zaman araligi grubu dis kimligi.
    public string? FromTimeframeGroupId { get; protected internal set; }

    // Varis zaman araligi grubu dis kimligi.
    public string? ToTimeframeGroupId { get; protected internal set; }

    // GTFS fare_product_id dis kimligi.
    public string GtfsFareProductId { get; protected internal set; } = default!;

    // Kural oncelik degeri.
    public int? RulePriority { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected FareLegRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareLegRule(Guid id, Guid feedId, string gtfsFareProductId) : base(id)
    {
        FeedId = feedId;
        GtfsFareProductId = gtfsFareProductId;
    }
}
