using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Timeframes;

// islevi: timeframes.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Zaman araligi verilerini Feed altinda tutar.
/// <summary>GTFS zaman araligi kaydini temsil eder.</summary>
public class Timeframe : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Zaman araligi grubu dis kimligi.
    public string TimeframeGroupId { get; internal set; } = default!;

    // GTFS service_id dis kimligi.
    public string GtfsServiceId { get; internal set; } = default!;

    // Zaman araligi baslangic saati (HH:mm:ss).
    public string? StartTime { get; internal set; }

    // Zaman araligi bitis saati (HH:mm:ss).
    public string? EndTime { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Timeframe()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Timeframe(Guid id, Guid? tenantId, Guid feedId, string timeframeGroupId, string gtfsServiceId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        TimeframeGroupId = timeframeGroupId;
        GtfsServiceId = gtfsServiceId;
    }
}
