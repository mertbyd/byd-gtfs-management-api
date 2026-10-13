using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.StopAreas;
using Byd.GtfsManagement.Interfaces.StopAreas;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.StopAreas;

namespace Byd.GtfsManagement.Managers.StopAreas;

// islevi: stop_areas.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS durak bolge iliskisi is kurallarini yonetir.</summary>
public class StopAreaManager(
    IStopAreaRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<StopArea, StopAreaCreateModel, StopAreaUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni durak bolge iliskisi kaydini kurar.
    public override async Task<StopArea> CreateAsync(
        StopAreaCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new StopArea(
            GuidGenerator.Create(), feed.TenantId, feed.Id,
            NormalizeRequiredText(model.GtfsAreaId), NormalizeRequiredText(model.GtfsStopId));
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        StopArea entity, StopAreaUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsAreaId = NormalizeRequiredText(model.GtfsAreaId);
        entity.GtfsStopId = NormalizeRequiredText(model.GtfsStopId);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(StopArea entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsAreaId = entity.GtfsAreaId;
        var gtfsStopId = entity.GtfsStopId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsAreaId == gtfsAreaId && value.GtfsStopId == gtfsStopId,
            entity.Id, cancellationToken);
    }
}
