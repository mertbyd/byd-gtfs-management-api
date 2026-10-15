using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.RouteNetworks;
using Byd.GtfsManagement.Interfaces.RouteNetworks;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.RouteNetworks;

namespace Byd.GtfsManagement.Managers.RouteNetworks;

// islevi: route_networks.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS hat ag iliskisi is kurallarini yonetir.</summary>
public class RouteNetworkManager(
    IRouteNetworkRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<RouteNetwork, RouteNetworkCreateModel, RouteNetworkUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni hat ag iliskisi kaydini kurar.
    public override async Task<RouteNetwork> CreateAsync(
        RouteNetworkCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new RouteNetwork(
            GuidGenerator.Create(), feed.Id,
            NormalizeRequiredText(model.GtfsNetworkId), NormalizeRequiredText(model.GtfsRouteId));
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        RouteNetwork entity, RouteNetworkUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsNetworkId = NormalizeRequiredText(model.GtfsNetworkId);
        entity.GtfsRouteId = NormalizeRequiredText(model.GtfsRouteId);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(RouteNetwork entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsRouteId = entity.GtfsRouteId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsRouteId == gtfsRouteId,
            entity.Id, cancellationToken);
    }
}
