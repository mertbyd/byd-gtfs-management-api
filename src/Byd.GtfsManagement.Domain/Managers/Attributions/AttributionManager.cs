using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Attributions;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.Attributions;
using Byd.GtfsManagement.Managers.Agencies;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Routes;
using Byd.GtfsManagement.Managers.Trips;
using Byd.GtfsManagement.Models.Attributions;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.Attributions;

// islevi: attributions.txt kurum, rol, guzergah, sefer ve atif kurallarini korur.
// sistemdeki gorevi: Her atfi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS atif is kurallarini yonetir.</summary>
public class AttributionManager(
    IAttributionRepository repository,
    FeedManager feedManager,
    AgencyManager agencyManager,
    RouteManager routeManager,
    TripManager tripManager)
    : GtfsFeedScopedManager<Attribution, AttributionCreateModel, AttributionUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni atif kaydini kurar.
    public override async Task<Attribution> CreateAsync(
        AttributionCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Attribution(
            GuidGenerator.Create(), feed.TenantId,
            feed.Id, NormalizeRequiredText(model.OrganizationName));
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut atfin kurum, rol veya bag alanlarini gunceller.
    public override Task UpdateAsync(
        Attribution entity, AttributionUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.OrganizationName = NormalizeRequiredText(model.OrganizationName);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Bagli ajans, guzergah ve seferin ayni Feed'de oldugunu dogrular; metin alanlarini kirpar.
    private async Task ApplyAsync(
        Attribution entity, AttributionInputModel model, CancellationToken cancellationToken = default)
    {
        await ValidateAgencyAsync(entity, model.AgencyId, cancellationToken);
        await ValidateRouteAsync(entity, model.RouteId, cancellationToken);
        await ValidateTripAsync(entity, model.TripId, cancellationToken);

        entity.AgencyId = model.AgencyId;
        entity.RouteId = model.RouteId;
        entity.TripId = model.TripId;

        entity.GtfsAttributionId = NormalizeOptionalText(model.GtfsAttributionId);
        entity.AttributionUrl = NormalizeOptionalText(model.AttributionUrl);
        entity.AttributionEmail = NormalizeOptionalText(model.AttributionEmail);
        entity.AttributionPhone = NormalizeOptionalText(model.AttributionPhone);
        entity.IsProducer = model.IsProducer;
        entity.IsOperator = model.IsOperator;
        entity.IsAuthority = model.IsAuthority;

        await CheckAttributionUniqueAsync(entity, cancellationToken);
    }

    // Bagli ajansin ayni Feed'de oldugunu dogrular.
    private async Task ValidateAgencyAsync(
        Attribution entity, Guid? agencyId, CancellationToken cancellationToken = default)
    {
        if (!agencyId.HasValue) return;
        var agency = await agencyManager.EnsureExistsInScopeAsync(agencyId.Value, cancellationToken);
        if (agency.FeedId != entity.FeedId)
            throw new BusinessException(AttributionExceptionCodes.AgencyOutOfFeed);
    }

    // Bagli guzergahin ayni Feed'de oldugunu dogrular.
    private async Task ValidateRouteAsync(
        Attribution entity, Guid? routeId, CancellationToken cancellationToken = default)
    {
        if (!routeId.HasValue) return;
        var route = await routeManager.EnsureExistsInScopeAsync(routeId.Value, cancellationToken);
        if (route.FeedId != entity.FeedId)
            throw new BusinessException(AttributionExceptionCodes.RouteOutOfFeed);
    }

    // Bagli seferin ayni Feed'de oldugunu dogrular.
    private async Task ValidateTripAsync(
        Attribution entity, Guid? tripId, CancellationToken cancellationToken = default)
    {
        if (!tripId.HasValue) return;
        var trip = await tripManager.EnsureExistsInScopeAsync(tripId.Value, cancellationToken);
        if (trip.FeedId != entity.FeedId)
            throw new BusinessException(AttributionExceptionCodes.TripOutOfFeed);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckAttributionUniqueAsync(Attribution entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsAttributionId = entity.GtfsAttributionId;
        var orgName = entity.OrganizationName;
        var agencyId = entity.AgencyId;
        var routeId = entity.RouteId;
        var tripId = entity.TripId;

        if (!string.IsNullOrEmpty(gtfsAttributionId))
        {
            return CheckAlreadyExistsAsync(
                value => value.FeedId == feedId && value.GtfsAttributionId == gtfsAttributionId,
                entity.Id, cancellationToken);
        }

        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId
                  && value.OrganizationName == orgName
                  && value.AgencyId == agencyId
                  && value.RouteId == routeId
                  && value.TripId == tripId,
            entity.Id, cancellationToken);
    }
}
