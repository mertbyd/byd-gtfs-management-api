using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Byd.GtfsManagement.Entities.Lookup.StopAccesses;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Gtfs;
using Byd.GtfsManagement.Extensions.Lookups;
using Byd.GtfsManagement.Interfaces.Pathways;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Interfaces.Services;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Interfaces.Stops;
using Byd.GtfsManagement.Interfaces.Transfers;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Levels;
using Byd.GtfsManagement.Models.Stops;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Stops;

// islevi: stops.txt kaydinin konum turune bagli zorunlu alanlarini ve istasyon hiyerarsisini yonetir.
// sistemdeki gorevi: GTFS durak kurallarinin tek sahibidir; kapsam ve silme guardlarini tabandan devralir.
public class StopManager(
    IStopRepository repository,
    FeedManager feedManager,
    IRepository<LocationType, int> locationTypeRepository,
    IRepository<WheelchairBoarding, int> wheelchairBoardingRepository,
    IRepository<StopAccess, int> stopAccessRepository,
    LevelManager levelManager,
    IServiceRepository serviceRepository,
    IStopTimeRepository stopTimeRepository,
    IPathwayRepository pathwayRepository,
    IRouteRepository routeRepository,
    ITransferRepository transferRepository)
    : GtfsFeedScopedManager<Stop, StopCreateModel, StopUpdateModel>(repository, feedManager)
{
    // Yeni duragi organizasyonun aktif Feed'inde kurar.
    public override async Task<Stop> CreateAsync(
        StopCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new Stop(
            GuidGenerator.Create(), feed.Id);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Yuklenmis duragin duzenlenebilir alanlarini ayni kurallardan gecirerek gunceller.
    public override Task UpdateAsync(
        Stop entity, StopUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Verilen ya da aktif Feed'in tum duraklarini harita icin ayrintilariyla getirir; aktif Feed yoksa bos liste doner.
    public virtual async Task<List<Stop>> GetMapAsync(Guid? feedId, CancellationToken cancellationToken = default)
    {
        if (feedId is not null)
            await ResolveFeedAsync(feedId.Value, cancellationToken);
        return await repository.GetListAsync(
            stop => feedId == null || stop.FeedId == feedId, true, cancellationToken);
    }

    // Duragi alt peronlari, ugrayan guzergahlari, pathway ve transfer kayitlariyla birlikte getirir.
    public virtual async Task<StopDetailModel> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var stop = await EnsureExistsInScopeAsync(id, cancellationToken);
        var children = await repository.GetListAsync(child => child.ParentStationId == id, true, cancellationToken);
        var stopIds = children.Select(child => child.Id).Append(id).ToList();
        return new StopDetailModel
        {
            Stop = await repository.GetAsync(value => value.Id == stop.Id, true, cancellationToken),
            Children = children,
            Routes = await routeRepository.GetAtStopsAsync(stopIds, cancellationToken),
            Pathways = await pathwayRepository.GetListAsync(
                pathway => stopIds.Contains(pathway.FromStopId) || stopIds.Contains(pathway.ToStopId), true, cancellationToken),
            Transfers = await transferRepository.GetListAsync(
                transfer => transfer.FromStopId != null && stopIds.Contains(transfer.FromStopId.Value)
                    || transfer.ToStopId != null && stopIds.Contains(transfer.ToStopId.Value), true, cancellationToken)
        };
    }

    // Duraktan ve istasyonsa alt peronlarindan servis gununde verilen saatten sonra kalkan seferleri getirir.
    public virtual async Task<List<StopTime>> GetDeparturesAsync(
        Guid id, StopDepartureQueryModel query, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        var stop = await EnsureExistsInScopeAsync(id, cancellationToken);
        var stopIds = (await repository.GetListAsync(child => child.ParentStationId == id, false, cancellationToken))
            .Select(child => child.Id).Append(id).ToList();
        var serviceIds = await serviceRepository.GetActiveIdsAsync(stop.FeedId, query.Date, cancellationToken);
        return serviceIds.Count == 0
            ? []
            : await stopTimeRepository.GetDeparturesAsync(stopIds, serviceIds, query.FromTime ?? GtfsTimeConsts.DayStart,
                query.Limit, cancellationToken);
    }

    // Duzenlenebilir alanlari normalize eder ve GTFS kurallarini dogrular.
    private async Task ApplyAsync(Stop entity, StopInputModel model, CancellationToken cancellationToken = default)
    {
        var locationCode = await ResolveLocationTypeCodeAsync(model.LocationTypeId, cancellationToken);
        entity.LocationTypeId = model.LocationTypeId;
        entity.WheelchairBoardingId = await wheelchairBoardingRepository.ResolveOptionalAsync(
            model.WheelchairBoardingId, cancellationToken);
        entity.ParentStationId = await ResolveParentStationIdAsync(entity, model, locationCode, cancellationToken);
        entity.LevelId = await ResolveLevelIdAsync(entity, model.LevelId, cancellationToken);
        entity.StopAccessId = await ResolveStopAccessIdAsync(entity, model.StopAccessId, locationCode, cancellationToken);
        entity.GtfsStopId = NormalizeRequiredText(model.GtfsStopId);
        entity.Code = NormalizeOptionalText(model.Code);
        entity.Name = NormalizeOptionalText(model.Name);
        entity.TtsName = NormalizeOptionalText(model.TtsName);
        entity.Description = NormalizeOptionalText(model.Description);
        entity.Latitude = model.Latitude;
        entity.Longitude = model.Longitude;
        entity.ZoneId = NormalizeOptionalText(model.ZoneId);
        entity.Url = NormalizeOptionalText(model.Url);
        entity.Timezone = NormalizeOptionalText(model.Timezone)
            .EnsureKnownTimezoneOrNull(StopExceptionCodes.TimezoneInvalid);
        entity.PlatformCode = NormalizeOptionalText(model.PlatformCode);
        EnsurePhysicalFieldsPresent(entity, locationCode);
        await CheckGtfsStopIdUniqueAsync(entity, cancellationToken);
    }

    // location_type kodunu lookup satirindan okur; bos deger GTFS'te durak veya peron anlamina gelir.
    private async Task<int> ResolveLocationTypeCodeAsync(int? locationTypeId, CancellationToken cancellationToken = default)
    {
        if (locationTypeId is null)
            return LocationTypeConsts.StopOrPlatformCode;
        await locationTypeRepository.EnsureExistsAsync(locationTypeId.Value, cancellationToken);
        var lookup = await locationTypeRepository.GetAsync(locationTypeId.Value, cancellationToken: cancellationToken);
        return lookup.Code;
    }

    // parent_station'in konum turune gore zorunlulugunu, istasyon olmasini ve ayni Feed'de bulunmasini dogrular.
    private async Task<Guid?> ResolveParentStationIdAsync(
        Stop entity, StopInputModel model, int locationCode, CancellationToken cancellationToken = default)
    {
        if (locationCode == LocationTypeConsts.StationCode && model.ParentStationId is not null)
            throw new BusinessException(StopExceptionCodes.ParentStationNotAllowed);
        if (model.ParentStationId is null)
            return RequiresParentStation(locationCode)
                ? throw new BusinessException(StopExceptionCodes.ParentStationRequired)
                : null;
        var parent = (await TryGetByIdAsync(model.ParentStationId.Value, cancellationToken: cancellationToken))!;
        if (parent.FeedId != entity.FeedId)
            throw new BusinessException(StopExceptionCodes.ParentStationOutOfFeed);
        if (await ResolveLocationTypeCodeAsync(parent.LocationTypeId, cancellationToken)
            != LocationTypeConsts.StationCode)
            throw new BusinessException(StopExceptionCodes.ParentStationNotAStation);
        return parent.Id;
    }

    // Kat verilmisse ayni Feed'de olmasini dogrular.
    private async Task<Guid?> ResolveLevelIdAsync(Stop entity, Guid? levelId, CancellationToken cancellationToken = default)
    {
        if (levelId is null)
            return null;
        var level = await levelManager.EnsureExistsInScopeAsync(levelId.Value, cancellationToken);
        return level.FeedId == entity.FeedId ? level.Id : throw new BusinessException(StopExceptionCodes.LevelOutOfFeed);
    }

    // stop_access yalniz bir istasyona bagli durak veya peronda verilebilir.
    private async Task<int?> ResolveStopAccessIdAsync(
        Stop entity, int? stopAccessId, int locationCode, CancellationToken cancellationToken = default)
    {
        if (stopAccessId is null)
            return null;
        if (locationCode != LocationTypeConsts.StopOrPlatformCode || entity.ParentStationId is null)
            throw new BusinessException(StopExceptionCodes.StopAccessNotAllowed);
        return await stopAccessRepository.EnsureExistsAsync(stopAccessId.Value, cancellationToken);
    }

    // GTFS'te giris, ara dugum ve binis alani kayitlari bir istasyona bagli olmak zorundadir.
    private static bool RequiresParentStation(int locationCode)
        => locationCode is LocationTypeConsts.EntranceOrExitCode
            or LocationTypeConsts.GenericNodeCode
            or LocationTypeConsts.BoardingAreaCode;

    // Durak, istasyon ve giris kayitlari icin ad ve koordinat zorunludur.
    private static void EnsurePhysicalFieldsPresent(Stop entity, int locationCode)
    {
        if (locationCode is not (LocationTypeConsts.StopOrPlatformCode
            or LocationTypeConsts.StationCode
            or LocationTypeConsts.EntranceOrExitCode))
            return;
        if (entity.Name is null)
            throw new BusinessException(StopExceptionCodes.NameRequired);
        if (entity.Latitude is null || entity.Longitude is null)
            throw new BusinessException(StopExceptionCodes.CoordinatesRequired);
    }

    // stop_id'nin Feed icinde tekil olmasini dogrular.
    private Task CheckGtfsStopIdUniqueAsync(Stop entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsStopId = entity.GtfsStopId;
        return CheckAlreadyExistsAsync(
            stop => stop.FeedId == feedId && stop.GtfsStopId == gtfsStopId,
            entity.Id,
            cancellationToken);
    }
}
