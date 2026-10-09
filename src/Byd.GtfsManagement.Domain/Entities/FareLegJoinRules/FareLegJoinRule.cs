using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareLegJoinRules;

// islevi: fare_leg_join_rules.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ucret etap birlestirme kurali verilerini Feed altinda tutar.
/// <summary>GTFS ucret etap birlestirme kurali kaydini temsil eder.</summary>
public class FareLegJoinRule : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Kalkis agi dis kimligi.
    public string? FromNetworkId { get; internal set; }

    // Varis agi dis kimligi.
    public string? ToNetworkId { get; internal set; }

    // Kalkis duragi dis kimligi.
    public string? FromStopId { get; internal set; }

    // Varis duragi dis kimligi.
    public string? ToStopId { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected FareLegJoinRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareLegJoinRule(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
