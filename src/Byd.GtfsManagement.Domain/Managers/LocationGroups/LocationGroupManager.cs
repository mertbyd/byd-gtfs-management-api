using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.LocationGroups;
using Byd.GtfsManagement.Interfaces.LocationGroups;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.LocationGroups;

namespace Byd.GtfsManagement.Managers.LocationGroups;

// islevi: location_groups.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS konum grubu is kurallarini yonetir.</summary>
public class LocationGroupManager(
    ILocationGroupRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<LocationGroup, LocationGroupCreateModel, LocationGroupUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni konum grubu kaydini kurar.
    public override async Task<LocationGroup> CreateAsync(
        LocationGroupCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new LocationGroup(GuidGenerator.Create(), feed.TenantId, feed.Id, NormalizeRequiredText(model.GtfsLocationGroupId));
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        LocationGroup entity, LocationGroupUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsLocationGroupId = NormalizeRequiredText(model.GtfsLocationGroupId);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(LocationGroup entity, LocationGroupInputModel model)
    {
        entity.LocationGroupName = NormalizeOptionalText(model.LocationGroupName);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(LocationGroup entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsLocationGroupId = entity.GtfsLocationGroupId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsLocationGroupId == gtfsLocationGroupId,
            entity.Id, cancellationToken);
    }
}
