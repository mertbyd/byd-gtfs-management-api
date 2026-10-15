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
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Zaman araligi grubu dis kimligi.
    public string TimeframeGroupId { get; protected internal set; } = default!;

    // GTFS service_id dis kimligi.
    public string GtfsServiceId { get; protected internal set; } = default!;

    // Zaman araligi baslangic saati (HH:mm:ss).
    public string? StartTime { get; protected internal set; }

    // Zaman araligi bitis saati (HH:mm:ss).
    public string? EndTime { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Timeframe()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Timeframe(Guid id, Guid feedId, string timeframeGroupId, string gtfsServiceId) : base(id)
    {
        FeedId = feedId;
        TimeframeGroupId = timeframeGroupId;
        GtfsServiceId = gtfsServiceId;
    }
}
