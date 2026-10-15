using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FareMedias;
using Byd.GtfsManagement.Interfaces.FareMedias;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.FareMedias;

namespace Byd.GtfsManagement.Managers.FareMedias;

// islevi: fare_media.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ucret medyasi is kurallarini yonetir.</summary>
public class FareMediaManager(
    IFareMediaRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<FareMedia, FareMediaCreateModel, FareMediaUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni ucret medyasi kaydini kurar.
    public override async Task<FareMedia> CreateAsync(
        FareMediaCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new FareMedia(GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.GtfsFareMediaId), model.FareMediaType);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        FareMedia entity, FareMediaUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsFareMediaId = NormalizeRequiredText(model.GtfsFareMediaId);
        entity.FareMediaType = model.FareMediaType;
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(FareMedia entity, FareMediaInputModel model)
    {
        entity.FareMediaName = NormalizeOptionalText(model.FareMediaName);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(FareMedia entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsFareMediaId = entity.GtfsFareMediaId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsFareMediaId == gtfsFareMediaId,
            entity.Id, cancellationToken);
    }
}
