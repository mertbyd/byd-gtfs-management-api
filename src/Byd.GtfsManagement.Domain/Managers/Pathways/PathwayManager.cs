using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.Pathways;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Models.Pathways;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.Pathways;

// islevi: pathways.txt yol kimligi, bagli duraklar, mod ve uzunluk kurallarini korur.
// sistemdeki gorevi: Her yolu Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS yuruyus yolu is kurallarini yonetir.</summary>
public class PathwayManager(
    IPathwayRepository repository,
    FeedManager feedManager,
    StopManager stopManager)
    : GtfsFeedScopedManager<Pathway, PathwayCreateModel, PathwayUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni yuruyus yolu kaydini kurar.
    public override async Task<Pathway> CreateAsync(
        PathwayCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new Pathway(
            GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.GtfsPathwayId));
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut yolun durak baglarini ve alanlarini gunceller.
    public override Task UpdateAsync(
        Pathway entity, PathwayUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsPathwayId = NormalizeRequiredText(model.GtfsPathwayId);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Durak baglarini dogrular, alanlari uygular ve pathway_id tekilligini denetler.
    private async Task ApplyAsync(
        Pathway entity, PathwayInputModel model, CancellationToken cancellationToken = default)
    {
        await ValidateStopsAsync(entity, model.FromStopId, model.ToStopId, cancellationToken);

        entity.FromStopId = model.FromStopId;
        entity.ToStopId = model.ToStopId;
        entity.PathwayMode = model.PathwayMode;
        entity.IsBidirectional = model.IsBidirectional;
        entity.Length = model.Length;
        entity.TraversalTime = model.TraversalTime;
        entity.StairCount = model.StairCount;
        entity.MaxSlope = model.MaxSlope;
        entity.MinWidth = model.MinWidth;
        entity.SignpostedAs = NormalizeOptionalText(model.SignpostedAs);
        entity.ReversedSignpostedAs = NormalizeOptionalText(model.ReversedSignpostedAs);

        await CheckPathwayUniqueAsync(entity, cancellationToken);
    }

    // Baslangic ve bitis duraklarinin kapsamda ve ayni Feed'de oldugunu dogrular.
    private async Task ValidateStopsAsync(
        Pathway entity, Guid fromStopId, Guid toStopId, CancellationToken cancellationToken = default)
    {
        var fromStop = await stopManager.EnsureExistsInScopeAsync(fromStopId, cancellationToken);
        var toStop = await stopManager.EnsureExistsInScopeAsync(toStopId, cancellationToken);

        if (fromStop.FeedId != entity.FeedId || toStop.FeedId != entity.FeedId)
            throw new BusinessException(PathwayExceptionCodes.StopOutOfFeed);
    }

    // Ayni Feed icinde pathway_id tekilligini saglar.
    private Task CheckPathwayUniqueAsync(Pathway entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsPathwayId = entity.GtfsPathwayId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsPathwayId == gtfsPathwayId,
            entity.Id, cancellationToken);
    }
}
