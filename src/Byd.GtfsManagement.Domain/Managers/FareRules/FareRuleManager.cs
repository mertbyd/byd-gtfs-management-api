using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FareRules;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.FareRules;
using Byd.GtfsManagement.Managers.FareAttributes;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Routes;
using Byd.GtfsManagement.Models.FareRules;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.FareRules;

// islevi: fare_rules.txt ucret sinifi, guzergah ve bolge esleme kurallarini korur.
// sistemdeki gorevi: Her ucret kuralini Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ucret kurali is kurallarini yonetir.</summary>
public class FareRuleManager(
    IFareRuleRepository repository,
    FeedManager feedManager,
    FareAttributeManager fareAttributeManager,
    RouteManager routeManager)
    : GtfsFeedScopedManager<FareRule, FareRuleCreateModel, FareRuleUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni ucret kurali kaydini kurar.
    public override async Task<FareRule> CreateAsync(
        FareRuleCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new FareRule(
            GuidGenerator.Create(), feed.Id, model.FareAttributeId);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut ucret kuralinin guzergah veya bolge alanlarini gunceller.
    public override Task UpdateAsync(
        FareRule entity, FareRuleUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.FareAttributeId = model.FareAttributeId;
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Ucret tanimi ile istege bagli guzergahin ayni Feed'de oldugunu dogrular; bolge kimliklerini kirpar.
    private async Task ApplyAsync(
        FareRule entity, FareRuleInputModel model, CancellationToken cancellationToken = default)
    {
        await ValidateFareAttributeAsync(entity, model.FareAttributeId, cancellationToken);
        await ValidateRouteAsync(entity, model.RouteId, cancellationToken);

        entity.OriginId = NormalizeOptionalText(model.OriginId);
        entity.DestinationId = NormalizeOptionalText(model.DestinationId);
        entity.ContainsId = NormalizeOptionalText(model.ContainsId);
        entity.RouteId = model.RouteId;

        await CheckRuleUniqueAsync(entity, cancellationToken);
    }

    // Bagli ucret taniminin varligini ve ayni Feed'de oldugunu dogrular.
    private async Task ValidateFareAttributeAsync(
        FareRule entity, Guid fareAttributeId, CancellationToken cancellationToken = default)
    {

        var fare = await fareAttributeManager.EnsureExistsInScopeAsync(fareAttributeId, cancellationToken);
        if (fare.FeedId != entity.FeedId)
            throw new BusinessException(FareRuleExceptionCodes.FareOutOfFeed);
    }

    // Bagli guzergahin ayni Feed'de oldugunu dogrular.
    private async Task ValidateRouteAsync(
        FareRule entity, Guid? routeId, CancellationToken cancellationToken = default)
    {
        if (!routeId.HasValue) return;

        var route = await routeManager.EnsureExistsInScopeAsync(routeId.Value, cancellationToken);
        if (route.FeedId != entity.FeedId)
            throw new BusinessException(FareRuleExceptionCodes.RouteOutOfFeed);
    }

    // Ayni Feed icinde ayni kural kombinasyonunun tekilligini saglar.
    private Task CheckRuleUniqueAsync(FareRule entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var fareAttributeId = entity.FareAttributeId;
        var routeId = entity.RouteId;
        var originId = entity.OriginId;
        var destinationId = entity.DestinationId;
        var containsId = entity.ContainsId;

        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId
                  && value.FareAttributeId == fareAttributeId
                  && value.RouteId == routeId
                  && value.OriginId == originId
                  && value.DestinationId == destinationId
                  && value.ContainsId == containsId,
            entity.Id, cancellationToken);
    }
}
