using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Timeframes;
using Byd.GtfsManagement.Interfaces.Timeframes;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Timeframes;

namespace Byd.GtfsManagement.Managers.Timeframes;

// islevi: timeframes.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS zaman araligi is kurallarini yonetir.</summary>
public class TimeframeManager(
    ITimeframeRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<Timeframe, TimeframeCreateModel, TimeframeUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni zaman araligi kaydini kurar.
    public override async Task<Timeframe> CreateAsync(
        TimeframeCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Timeframe(GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.TimeframeGroupId), NormalizeRequiredText(model.GtfsServiceId));
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        Timeframe entity, TimeframeUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.TimeframeGroupId = NormalizeRequiredText(model.TimeframeGroupId);
        entity.GtfsServiceId = NormalizeRequiredText(model.GtfsServiceId);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(Timeframe entity, TimeframeInputModel model)
    {
        entity.StartTime = NormalizeOptionalText(model.StartTime);
        entity.EndTime = NormalizeOptionalText(model.EndTime);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(Timeframe entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var timeframeGroupId = entity.TimeframeGroupId;
        var gtfsServiceId = entity.GtfsServiceId;
        var startTime = entity.StartTime;
        var endTime = entity.EndTime;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.TimeframeGroupId == timeframeGroupId && value.GtfsServiceId == gtfsServiceId && value.StartTime == startTime && value.EndTime == endTime,
            entity.Id, cancellationToken);
    }
}
