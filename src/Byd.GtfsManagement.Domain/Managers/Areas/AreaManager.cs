using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Areas;
using Byd.GtfsManagement.Interfaces.Areas;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Areas;

namespace Byd.GtfsManagement.Managers.Areas;

// islevi: areas.txt bolge kimligi ve bolge adi kurallarini korur.
// sistemdeki gorevi: Her bolgeyi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS bolge is kurallarini yonetir.</summary>
public class AreaManager(
    IAreaRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<Area, AreaCreateModel, AreaUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni bolge kaydini kurar.
    public override async Task<Area> CreateAsync(
        AreaCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new Area(
            GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.GtfsAreaId));
        Apply(entity, model);
        await CheckAreaUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut bolgenin dis kimlik veya ad alanlarini gunceller.
    public override async Task UpdateAsync(
        Area entity, AreaUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsAreaId = NormalizeRequiredText(model.GtfsAreaId);
        Apply(entity, model);
        await CheckAreaUniqueAsync(entity, cancellationToken);
    }

    // Bolge adini kirpar.
    private static void Apply(Area entity, AreaInputModel model)
        => entity.AreaName = NormalizeOptionalText(model.AreaName);

    // Ayni Feed icinde GTFS bolge kimliginin tekilligini saglar.
    private Task CheckAreaUniqueAsync(Area entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsAreaId = entity.GtfsAreaId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsAreaId == gtfsAreaId,
            entity.Id, cancellationToken);
    }
}
