using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.GtfsManagement.Entities.Lookup.BikesAlloweds;
using Byd.GtfsManagement.Entities.Lookup.CarsAlloweds;
using Byd.GtfsManagement.Entities.Lookup.TripDirections;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Lookups;
using Byd.GtfsManagement.Interfaces.CalendarDates;
using Byd.GtfsManagement.Interfaces.Calendars;
using Byd.GtfsManagement.Interfaces.Frequencies;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Interfaces.Services;
using Byd.GtfsManagement.Interfaces.Shapes;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Interfaces.Trips;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Trips;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Trips;

// islevi: trips.txt kaydinin guzergah ve servis baglarini ayni Feed'de tutar, kimlik tekilligini dogrular.
// sistemdeki gorevi: Sefer is kurallarinin tek sahibidir; kapsam ve silme guardlarini tabandan devralir.
public class TripManager(
    ITripRepository repository,
    FeedManager feedManager,
    IRouteRepository routeRepository,
    IServiceRepository serviceRepository,
    IRepository<TripDirection, int> tripDirectionRepository,
    IRepository<WheelchairBoarding, int> wheelchairBoardingRepository,
    IRepository<BikesAllowed, int> bikesAllowedRepository,
    IRepository<CarsAllowed, int> carsAllowedRepository,
    IStopTimeRepository stopTimeRepository,
    IShapeRepository shapeRepository,
    IFrequencyRepository frequencyRepository,
    ICalendarRepository calendarRepository,
    ICalendarDateRepository calendarDateRepository)
    : GtfsFeedScopedManager<Trip, TripCreateModel, TripUpdateModel>(repository, feedManager)
{
    // Seferi duraklari, sikliklari, sekli ve servisinin calisma gunleriyle birlikte getirir.
    public virtual async Task<TripDetailModel> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await EnsureExistsInScopeAsync(id, cancellationToken);
        var trip = await repository.GetAsync(value => value.Id == id, true, cancellationToken);
        return new TripDetailModel
        {
            Trip = trip,
            Stops = await stopTimeRepository.GetTripStopsAsync(id, cancellationToken),
            Frequencies = await frequencyRepository.GetListAsync(frequency => frequency.TripId == id, true, cancellationToken),
            Shape = trip.GtfsShapeId is { } shapeId
                ? await shapeRepository.GetPointsAsync(trip.FeedId, shapeId, cancellationToken)
                : [],
            Calendar = await calendarRepository.FindAsync(calendar => calendar.ServiceId == trip.ServiceId, true, cancellationToken),
            CalendarDates = await calendarDateRepository.GetListAsync(
                calendarDate => calendarDate.ServiceId == trip.ServiceId, true, cancellationToken)
        };
    }

    // Hedef Feed'i kapsamda dogrulayip yeni seferi kurar.
    public override async Task<Trip> CreateAsync(
        TripCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Trip(
            GuidGenerator.Create(), feed.TenantId, feed.Id);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Yuklenmis seferin duzenlenebilir alanlarini ayni kurallardan gecirerek gunceller.
    public override Task UpdateAsync(
        Trip entity, TripUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Duzenlenebilir alanlari normalize eder ve GTFS kurallarini dogrular.
    private async Task ApplyAsync(Trip entity, TripInputModel model, CancellationToken cancellationToken = default)
    {
        entity.RouteId = await ResolveRouteIdAsync(entity, model.RouteId, cancellationToken);
        entity.ServiceId = await ResolveServiceIdAsync(entity, model.ServiceId, cancellationToken);
        entity.TripDirectionId = await tripDirectionRepository.ResolveOptionalAsync(
            model.TripDirectionId, cancellationToken);
        entity.WheelchairAccessibleId = await wheelchairBoardingRepository.ResolveOptionalAsync(
            model.WheelchairAccessibleId, cancellationToken);
        entity.BikesAllowedId = await bikesAllowedRepository.ResolveOptionalAsync(
            model.BikesAllowedId, cancellationToken);
        entity.CarsAllowedId = await carsAllowedRepository.ResolveOptionalAsync(
            model.CarsAllowedId, cancellationToken);
        entity.GtfsTripId = NormalizeRequiredText(model.GtfsTripId);
        entity.GtfsShapeId = NormalizeOptionalText(model.GtfsShapeId);
        entity.Headsign = NormalizeOptionalText(model.Headsign);
        entity.ShortName = NormalizeOptionalText(model.ShortName);
        entity.BlockId = NormalizeOptionalText(model.BlockId);
        await CheckGtfsTripIdUniqueAsync(entity, cancellationToken);
    }

    // Guzergahin var oldugunu ve seferle ayni Feed'de bulundugunu dogrular.
    private async Task<Guid> ResolveRouteIdAsync(Trip entity, Guid routeId, CancellationToken cancellationToken = default)
    {
        var route = await routeRepository.FindAsync(
            candidate => candidate.Id == routeId, cancellationToken: cancellationToken);
        if (route is null)
            throw new BusinessException(GeneralExceptionCodes.EntityNotFound);
        if (route.FeedId != entity.FeedId)
            throw new BusinessException(TripExceptionCodes.RouteOutOfFeed);
        return route.Id;
    }

    // Servisin var oldugunu ve seferle ayni Feed'de bulundugunu dogrular.
    private async Task<Guid> ResolveServiceIdAsync(Trip entity, Guid serviceId, CancellationToken cancellationToken = default)
    {
        var service = await serviceRepository.FindAsync(
            candidate => candidate.Id == serviceId, cancellationToken: cancellationToken);
        if (service is null)
            throw new BusinessException(GeneralExceptionCodes.EntityNotFound);
        if (service.FeedId != entity.FeedId)
            throw new BusinessException(TripExceptionCodes.ServiceOutOfFeed);
        return service.Id;
    }

    // trip_id'nin Feed icinde tekil olmasini dogrular.
    private Task CheckGtfsTripIdUniqueAsync(Trip entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsTripId = entity.GtfsTripId;
        return CheckAlreadyExistsAsync(
            trip => trip.FeedId == feedId && trip.GtfsTripId == gtfsTripId,
            entity.Id,
            cancellationToken);
    }
}
