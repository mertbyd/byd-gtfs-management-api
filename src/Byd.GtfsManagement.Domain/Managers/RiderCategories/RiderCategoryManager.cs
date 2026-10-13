using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.RiderCategories;
using Byd.GtfsManagement.Interfaces.RiderCategories;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.RiderCategories;

namespace Byd.GtfsManagement.Managers.RiderCategories;

// islevi: rider_categories.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS yolcu kategorisi is kurallarini yonetir.</summary>
public class RiderCategoryManager(
    IRiderCategoryRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<RiderCategory, RiderCategoryCreateModel, RiderCategoryUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni yolcu kategorisi kaydini kurar.
    public override async Task<RiderCategory> CreateAsync(
        RiderCategoryCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new RiderCategory(GuidGenerator.Create(), feed.TenantId, feed.Id, NormalizeRequiredText(model.GtfsRiderCategoryId));
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        RiderCategory entity, RiderCategoryUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsRiderCategoryId = NormalizeRequiredText(model.GtfsRiderCategoryId);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(RiderCategory entity, RiderCategoryInputModel model)
    {
        entity.RiderCategoryName = NormalizeRequiredText(model.RiderCategoryName);
        entity.IsDefaultFareCategory = model.IsDefaultFareCategory;
        entity.EligibilityUrl = NormalizeOptionalText(model.EligibilityUrl);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(RiderCategory entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsRiderCategoryId = entity.GtfsRiderCategoryId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsRiderCategoryId == gtfsRiderCategoryId,
            entity.Id, cancellationToken);
    }
}
