using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Interfaces.Levels;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Levels;

namespace Byd.GtfsManagement.Managers.Levels;

// islevi: levels.txt kat kimligi, dizin, ad ve tekillik kurallarini korur.
// sistemdeki gorevi: Her kati Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS kat is kurallarini yonetir.</summary>
public class LevelManager(
    ILevelRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<Level, LevelCreateModel, LevelUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni kat kaydini kurar.
    public override async Task<Level> CreateAsync(
        LevelCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Level(
            GuidGenerator.Create(), feed.TenantId,
            feed.Id, NormalizeRequiredText(model.GtfsLevelId));
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut katin alanlarini gunceller.
    public override Task UpdateAsync(
        Level entity, LevelUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsLevelId = NormalizeRequiredText(model.GtfsLevelId);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Kat dizinini ve kirpilmis adini uygular.
    private async Task ApplyAsync(
        Level entity, LevelInputModel model, CancellationToken cancellationToken = default)
    {
        entity.LevelIndex = model.LevelIndex;
        entity.Name = NormalizeOptionalText(model.Name);

        await CheckLevelUniqueAsync(entity, cancellationToken);
    }

    // Ayni Feed icinde level_id tekilligini saglar.
    private Task CheckLevelUniqueAsync(Level entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsLevelId = entity.GtfsLevelId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsLevelId == gtfsLevelId,
            entity.Id, cancellationToken);
    }
}
