using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.StopAreas;

// islevi: stop_areas.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Durak bolge iliskisi verilerini Feed altinda tutar.
/// <summary>GTFS durak bolge iliskisi kaydini temsil eder.</summary>
public class StopArea : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS area_id dis kimligi.
    public string GtfsAreaId { get; internal set; } = default!;

    // GTFS stop_id dis kimligi.
    public string GtfsStopId { get; internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected StopArea()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public StopArea(Guid id, Guid? tenantId, Guid feedId, string gtfsAreaId, string gtfsStopId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsAreaId = gtfsAreaId;
        GtfsStopId = gtfsStopId;
    }
}
