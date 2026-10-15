using System.Threading.Tasks;
using System.Threading;
using System;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Byd.GtfsManagement.Entities.Lookup.PickupDropOffTypes;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Gtfs;
using Byd.GtfsManagement.Extensions.Lookups;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.LocationGroups;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Managers.Trips;
using Byd.GtfsManagement.Models.StopTimes;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.StopTimes;

// islevi: stop_times.txt satirinin Feed, durak turu, zaman ve sira kurallarini korur.
// sistemdeki gorevi: Trip ve Stop arasindaki GTFS iliskisinin tek is kurali sahibidir.
/// <summary>Sefer durak zamaninin kapsam ve GTFS kurallarini yonetir.</summary>
public class StopTimeManager(
    IStopTimeRepository repository,
    FeedManager feedManager,
    TripManager tripManager,
    StopManager stopManager,
    IRepository<LocationType, int> locationTypeRepository,
    IRepository<PickupDropOffType, int> pickupDropOffTypeRepository,
    IRepository<ContinuousPickupDropOff, int> continuousPickupDropOffRepository,
    LocationGroupManager locationGroupManager)
    : GtfsFeedScopedManager<StopTime, StopTimeCreateModel, StopTimeUpdateModel>(
        repository, feedManager)
{
    // Seferin kapsamindan yeni durak zamani kaydini kurar.
    public override async Task<StopTime> CreateAsync(
        StopTimeCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var trip = await tripManager.EnsureExistsInScopeAsync(model.TripId, cancellationToken);
        var entity = new StopTime(
            GuidGenerator.Create(), trip.FeedId)
        {
            TripId = trip.Id
        };
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut kaydin degistirilebilir alanlarini ayni kurallardan gecirir.
    public override Task UpdateAsync(
        StopTime entity, StopTimeUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Durak bagini, zamanlari, binis davranisini ve sira tekilligini uygular.
    private async Task ApplyAsync(
        StopTime entity, StopTimeInputModel model, CancellationToken cancellationToken = default)
    {
        entity.StopId = model.StopId is { } stopId ? await ResolveStopIdAsync(entity, stopId, cancellationToken) : null;
        entity.LocationGroupId = await ResolveLocationGroupIdAsync(entity, model.LocationGroupId, cancellationToken);
        entity.StopSequence = model.StopSequence;
        ApplySchedule(entity, model);
        entity.StopHeadsign = NormalizeOptionalText(model.StopHeadsign);
        await ApplyBoardingAsync(entity, model, cancellationToken);
        entity.PickupBookingRuleId = NormalizeOptionalText(model.PickupBookingRuleId);
        entity.DropOffBookingRuleId = NormalizeOptionalText(model.DropOffBookingRuleId);
        await CheckTripSequenceUniqueAsync(entity, cancellationToken);
    }

    // Durak ziyaretinin kesin saatlerini veya talep penceresini uygular.
    private static void ApplySchedule(StopTime entity, StopTimeInputModel model)
    {
        entity.ArrivalTime = NormalizeOptionalText(model.ArrivalTime);
        entity.DepartureTime = NormalizeOptionalText(model.DepartureTime);
        entity.StartPickupDropOffWindow = NormalizeOptionalText(model.StartPickupDropOffWindow);
        entity.EndPickupDropOffWindow = NormalizeOptionalText(model.EndPickupDropOffWindow);
        entity.ShapeDistTraveled = model.ShapeDistTraveled;
        entity.Timepoint = model.Timepoint;
        EnsureWindowOrder(entity);
    }

    // Istege bagli binis ve inis lookup kayitlarini dogrular.
    private async Task ApplyBoardingAsync(
        StopTime entity, StopTimeInputModel model, CancellationToken cancellationToken = default)
    {
        entity.PickupTypeId = await pickupDropOffTypeRepository.ResolveOptionalAsync(
            model.PickupTypeId, cancellationToken);
        entity.DropOffTypeId = await pickupDropOffTypeRepository.ResolveOptionalAsync(
            model.DropOffTypeId, cancellationToken);
        entity.ContinuousPickupId = await continuousPickupDropOffRepository.ResolveOptionalAsync(
            model.ContinuousPickupId, cancellationToken);
        entity.ContinuousDropOffId = await continuousPickupDropOffRepository.ResolveOptionalAsync(
            model.ContinuousDropOffId, cancellationToken);
    }

    // Duragin erisilebilir ve ayni Feed icinde bir durak/peron oldugunu dogrular.
    private async Task<Guid> ResolveStopIdAsync(
        StopTime entity, Guid stopId, CancellationToken cancellationToken = default)
    {
        var stop = await stopManager.EnsureExistsInScopeAsync(stopId, cancellationToken);
        if (stop.FeedId != entity.FeedId)
            throw new BusinessException(StopTimeExceptionCodes.StopOutOfFeed);
        if (stop.LocationTypeId is not null)
        {
            var locationType = await locationTypeRepository.GetAsync(
                stop.LocationTypeId.Value, cancellationToken: cancellationToken);
            if (locationType.Code != LocationTypeConsts.StopOrPlatformCode)
                throw new BusinessException(StopTimeExceptionCodes.StopNotPlatform);
        }
        return stop.Id;
    }

    // Talep penceresinin baslangicinin bitisinden once oldugunu servis gunu saatine gore dogrular.
    private static void EnsureWindowOrder(StopTime entity)
    {
        if (entity.StartPickupDropOffWindow is { } start && entity.EndPickupDropOffWindow is { } end
            && start.CompareGtfsTimes(end) >= 0)
            throw new BusinessException(StopTimeExceptionCodes.WindowInvalid);
    }

    // Konum grubu verilmisse ayni Feed'de olmasini dogrular.
    private async Task<Guid?> ResolveLocationGroupIdAsync(
        StopTime entity, Guid? locationGroupId, CancellationToken cancellationToken = default)
    {
        if (locationGroupId is null)
            return null;
        var group = await locationGroupManager.EnsureExistsInScopeAsync(locationGroupId.Value, cancellationToken);
        return group.FeedId == entity.FeedId
            ? group.Id
            : throw new BusinessException(StopTimeExceptionCodes.LocationGroupOutOfFeed);
    }

    // Bir seferde ayni stop_sequence degerinin tekrarini engeller.
    private Task CheckTripSequenceUniqueAsync(StopTime entity, CancellationToken cancellationToken = default)
    {
        var tripId = entity.TripId;
        var stopSequence = entity.StopSequence;
        return CheckAlreadyExistsAsync(
            stopTime => stopTime.TripId == tripId && stopTime.StopSequence == stopSequence,
            entity.Id,
            cancellationToken);
    }
}
