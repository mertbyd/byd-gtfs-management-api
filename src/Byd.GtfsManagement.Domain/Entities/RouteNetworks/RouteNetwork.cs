using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.RouteNetworks;

// islevi: route_networks.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Hat ag iliskisi verilerini Feed altinda tutar.
/// <summary>GTFS hat ag iliskisi kaydini temsil eder.</summary>
public class RouteNetwork : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS network_id dis kimligi.
    public string GtfsNetworkId { get; internal set; } = default!;

    // GTFS route_id dis kimligi.
    public string GtfsRouteId { get; internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected RouteNetwork()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public RouteNetwork(Guid id, Guid? tenantId, Guid feedId, string gtfsNetworkId, string gtfsRouteId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsNetworkId = gtfsNetworkId;
        GtfsRouteId = gtfsRouteId;
    }
}
