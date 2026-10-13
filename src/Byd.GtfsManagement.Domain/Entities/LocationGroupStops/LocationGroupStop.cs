using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.LocationGroupStops;

// islevi: location_group_stops.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Konum grubu durak iliskisi verilerini Feed altinda tutar.
/// <summary>GTFS konum grubu durak iliskisi kaydini temsil eder.</summary>
public class LocationGroupStop : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS location_group_id dis kimligi.
    public string GtfsLocationGroupId { get; internal set; } = default!;

    // GTFS stop_id dis kimligi.
    public string GtfsStopId { get; internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected LocationGroupStop()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public LocationGroupStop(Guid id, Guid? tenantId, Guid feedId, string gtfsLocationGroupId, string gtfsStopId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsLocationGroupId = gtfsLocationGroupId;
        GtfsStopId = gtfsStopId;
    }
}
