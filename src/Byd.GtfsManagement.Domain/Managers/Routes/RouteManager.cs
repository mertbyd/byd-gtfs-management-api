using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Byd.GtfsManagement.Entities.Lookup.RouteTypes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Lookups;
using Byd.GtfsManagement.Interfaces.Agencies;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Interfaces.Shapes;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Interfaces.Trips;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Networks;
using Byd.GtfsManagement.Models.Routes;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Routes;

// islevi: routes.txt kaydinin ajans secimini ve GTFS ad kurallarini yonetir.
// sistemdeki gorevi: Guzergah is kurallarinin tek sahibidir; kapsam ve silme guardlarini tabandan devralir.
public class RouteManager(
    IRouteRepository repository,
    FeedManager feedManager,
    IAgencyRepository agencyRepository,
    IRepository<RouteType, int> routeTypeRepository,
    IRepository<ContinuousPickupDropOff, int> continuousRepository,
    IRepository<CemvSupport, int> cemvSupportRepository,
    NetworkManager networkManager,
    ITripRepository tripRepository,
    IStopTimeRepository stopTimeRepository,
    IShapeRepository shapeRepository)
    : GtfsFeedScopedManager<Route, RouteCreateModel, RouteUpdateModel>(repository, feedManager)
{
    // Hedef Feed'i kapsamda dogrulayip yeni guzergahi kurar.
    public override async Task<Route> CreateAsync(
        RouteCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Route(
            GuidGenerator.Create(), feed.Id);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Yuklenmis guzergahin duzenlenebilir alanlarini ayni kurallardan gecirerek gunceller.
    public override Task UpdateAsync(
        Route entity, RouteUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Guzergahi, her sefer kalibinin ornek seferindeki duraklar ve sekil noktalariyla birlikte getirir.
    public virtual async Task<RouteDetailModel> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await EnsureExistsInScopeAsync(id, cancellationToken);
        var route = await repository.GetAsync(value => value.Id == id, true, cancellationToken);
        var patterns = await tripRepository.GetPatternsAsync(id, cancellationToken);
        foreach (var pattern in patterns)
            await DescribePatternAsync(route, pattern, cancellationToken);
        return new RouteDetailModel { Route = route, Patterns = patterns };
    }

    // Kalibin ornek seferindeki duraklari ve varsa seklini doldurur.
    private async Task DescribePatternAsync(Route route, RoutePatternModel pattern, CancellationToken cancellationToken = default)
    {
        pattern.Stops = await stopTimeRepository.GetTripStopsAsync(pattern.Trip.Id, cancellationToken);
        if (pattern.Trip.GtfsShapeId is { } shapeId)
            pattern.Shape = await shapeRepository.GetPointsAsync(route.FeedId, shapeId, cancellationToken);
    }

    // Duzenlenebilir alanlari normalize eder ve GTFS kurallarini dogrular.
    private async Task ApplyAsync(Route entity, RouteInputModel model, CancellationToken cancellationToken = default)
    {
        entity.AgencyId = await ResolveAgencyIdAsync(entity, model.AgencyId, cancellationToken);
        entity.RouteTypeId = await routeTypeRepository.EnsureExistsAsync(model.RouteTypeId, cancellationToken);
        entity.ContinuousPickupId = await continuousRepository.ResolveOptionalAsync(
            model.ContinuousPickupId, cancellationToken);
        entity.ContinuousDropOffId = await continuousRepository.ResolveOptionalAsync(
            model.ContinuousDropOffId, cancellationToken);
        entity.NetworkId = await ResolveNetworkIdAsync(entity, model.NetworkId, cancellationToken);
        entity.CemvSupportId = await cemvSupportRepository.ResolveOptionalAsync(model.CemvSupportId, cancellationToken);
        entity.GtfsRouteId = NormalizeRequiredText(model.GtfsRouteId);
        entity.ShortName = NormalizeOptionalText(model.ShortName);
        entity.LongName = NormalizeOptionalText(model.LongName);
        entity.Description = NormalizeOptionalText(model.Description);
        entity.Url = NormalizeOptionalText(model.Url);
        entity.Color = NormalizeOptionalText(model.Color);
        entity.TextColor = NormalizeOptionalText(model.TextColor);
        entity.SortOrder = model.SortOrder;
        EnsureNamePresent(entity);
        await CheckGtfsRouteIdUniqueAsync(entity, cancellationToken);
    }

    // Ucret agi verilmisse ayni Feed'de olmasini dogrular.
    private async Task<Guid?> ResolveNetworkIdAsync(
        Route entity, Guid? networkId, CancellationToken cancellationToken = default)
    {
        if (networkId is null)
            return null;
        var network = await networkManager.EnsureExistsInScopeAsync(networkId.Value, cancellationToken);
        return network.FeedId == entity.FeedId
            ? network.Id
            : throw new BusinessException(RouteExceptionCodes.NetworkOutOfFeed);
    }

    // Ajans verilmisse ayni Feed'de olmasini, verilmemisse Feed'de tek ajans bulunmasini dogrular.
    private async Task<Guid?> ResolveAgencyIdAsync(
        Route entity, Guid? agencyId, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        if (agencyId is null)
            return await HasSingleAgencyAsync(feedId, cancellationToken)
                ? null
                : throw new BusinessException(RouteExceptionCodes.AgencyRequired);
        var agency = await agencyRepository.FindAsync(
            candidate => candidate.Id == agencyId.Value, cancellationToken: cancellationToken);
        if (agency is null)
            throw new BusinessException(GeneralExceptionCodes.EntityNotFound);
        if (agency.FeedId != feedId)
            throw new BusinessException(RouteExceptionCodes.AgencyOutOfFeed);
        return agency.Id;
    }

    // GTFS'te agency_id yalnizca Feed'de birden fazla ajans varsa zorunludur.
    private async Task<bool> HasSingleAgencyAsync(Guid feedId, CancellationToken cancellationToken = default)
    {
        var agencies = await agencyRepository.GetListAsync(
            agency => agency.FeedId == feedId, cancellationToken: cancellationToken);
        return agencies.Count <= 1;
    }

    // GTFS kisa ad ile uzun addan en az birinin dolu olmasini zorunlu kilar.
    private static void EnsureNamePresent(Route entity)
    {
        if (entity.ShortName is null && entity.LongName is null)
            throw new BusinessException(RouteExceptionCodes.NameRequired);
    }

    // route_id'nin Feed icinde tekil olmasini dogrular.
    private Task CheckGtfsRouteIdUniqueAsync(Route entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsRouteId = entity.GtfsRouteId;
        return CheckAlreadyExistsAsync(
            route => route.FeedId == feedId && route.GtfsRouteId == gtfsRouteId,
            entity.Id,
            cancellationToken);
    }
}
