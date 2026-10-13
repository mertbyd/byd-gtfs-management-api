using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FareProducts;
using Byd.GtfsManagement.Interfaces.FareProducts;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.FareProducts;

namespace Byd.GtfsManagement.Managers.FareProducts;

// islevi: fare_products.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ucret urunu is kurallarini yonetir.</summary>
public class FareProductManager(
    IFareProductRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<FareProduct, FareProductCreateModel, FareProductUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni ucret urunu kaydini kurar.
    public override async Task<FareProduct> CreateAsync(
        FareProductCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new FareProduct(GuidGenerator.Create(), feed.TenantId, feed.Id, NormalizeRequiredText(model.GtfsFareProductId), model.Amount, NormalizeRequiredText(model.Currency));
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        FareProduct entity, FareProductUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsFareProductId = NormalizeRequiredText(model.GtfsFareProductId);
        entity.Amount = model.Amount;
        entity.Currency = NormalizeRequiredText(model.Currency);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(FareProduct entity, FareProductInputModel model)
    {
        entity.FareProductName = NormalizeOptionalText(model.FareProductName);
        entity.GtfsRiderCategoryId = NormalizeOptionalText(model.GtfsRiderCategoryId);
        entity.GtfsFareMediaId = NormalizeOptionalText(model.GtfsFareMediaId);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(FareProduct entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsFareProductId = entity.GtfsFareProductId;
        var gtfsRiderCategoryId = entity.GtfsRiderCategoryId;
        var gtfsFareMediaId = entity.GtfsFareMediaId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsFareProductId == gtfsFareProductId && value.GtfsRiderCategoryId == gtfsRiderCategoryId && value.GtfsFareMediaId == gtfsFareMediaId,
            entity.Id, cancellationToken);
    }
}
