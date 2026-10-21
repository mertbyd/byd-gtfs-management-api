using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.LocationGroupStops;
using Byd.GtfsManagement.Interfaces.LocationGroupStops;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.LocationGroupStops;

namespace Byd.GtfsManagement.Managers.LocationGroupStops;

// islevi: location_group_stops.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS konum grubu durak iliskisi is kurallarini yonetir.</summary>
public class LocationGroupStopManager(
    ILocationGroupStopRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<LocationGroupStop, LocationGroupStopCreateModel, LocationGroupStopUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni konum grubu durak iliskisi kaydini kurar.
    public override async Task<LocationGroupStop> CreateAsync(
        LocationGroupStopCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new LocationGroupStop(
            GuidGenerator.Create(), feed.Id,
            NormalizeRequiredText(model.GtfsLocationGroupId), NormalizeRequiredText(model.GtfsStopId));
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        LocationGroupStop entity, LocationGroupStopUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsLocationGroupId = NormalizeRequiredText(model.GtfsLocationGroupId);
        entity.GtfsStopId = NormalizeRequiredText(model.GtfsStopId);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(LocationGroupStop entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsLocationGroupId = entity.GtfsLocationGroupId;
        var gtfsStopId = entity.GtfsStopId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsLocationGroupId == gtfsLocationGroupId && value.GtfsStopId == gtfsStopId,
            entity.Id, cancellationToken);
    }
}
