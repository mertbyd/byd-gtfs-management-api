using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Transfers;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Interfaces.Transfers;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Routes;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Managers.Trips;
using Byd.GtfsManagement.Models.Transfers;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.Transfers;

// islevi: transfers.txt kosullu bag, konum, guzergah ve bagli sefer kurallarini korur.
// sistemdeki gorevi: Aktarma is kurallarini ve GTFS satir tekilligini tek Manager'da toplar.
/// <summary>GTFS aktarma kurallarini yonetir.</summary>
public class TransferManager(
    ITransferRepository repository,
    FeedManager feedManager,
    StopManager stopManager,
    RouteManager routeManager,
    TripManager tripManager,
    IRepository<LocationType, int> locationTypeRepository)
    : GtfsFeedScopedManager<Transfer, TransferCreateModel, TransferUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni aktarma kaydini kurar.
    public override async Task<Transfer> CreateAsync(
        TransferCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new Transfer(
            GuidGenerator.Create(), feed.Id);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut aktarmanin Feed'ini koruyarak GTFS alanlarini gunceller.
    public override Task UpdateAsync(
        Transfer entity, TransferUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Baglari Feed kapsaminda cozer ve tekil aktarma anahtarini dogrular.
    private async Task ApplyAsync(
        Transfer entity, TransferInputModel model, CancellationToken cancellationToken = default)
    {
        entity.TransferType = model.TransferType ?? TransferConsts.RecommendedCode;
        entity.MinTransferTime = model.MinTransferTime;
        await ApplyStopReferencesAsync(entity, model, cancellationToken);
        await ApplyJourneyReferencesAsync(entity, model, cancellationToken);
        await CheckTransferUniqueAsync(entity, cancellationToken);
    }

    // Istege bagli duraklari kapsamda cozer ve izinli konum turlerini dogrular.
    private async Task ApplyStopReferencesAsync(
        Transfer entity, TransferInputModel model, CancellationToken cancellationToken = default)
    {
        var from = await ResolveReferenceAsync<Stop>(model.FromStopId,
            stopManager.EnsureExistsInScopeAsync, entity.FeedId, cancellationToken);
        var to = await ResolveReferenceAsync<Stop>(model.ToStopId,
            stopManager.EnsureExistsInScopeAsync, entity.FeedId, cancellationToken);
        await EnsureStopTypeAsync(from, entity.TransferType, cancellationToken);
        await EnsureStopTypeAsync(to, entity.TransferType, cancellationToken);
        entity.FromStopId = from?.Id;
        entity.ToStopId = to?.Id;
    }

    // Guzergah ve seferleri kapsamda cozer, eslestirir ve bagli sefer kurallarini uygular.
    private async Task ApplyJourneyReferencesAsync(
        Transfer entity, TransferInputModel model, CancellationToken cancellationToken = default)
    {
        var fromRoute = await ResolveReferenceAsync<Route>(model.FromRouteId,
            routeManager.EnsureExistsInScopeAsync, entity.FeedId, cancellationToken);
        var toRoute = await ResolveReferenceAsync<Route>(model.ToRouteId,
            routeManager.EnsureExistsInScopeAsync, entity.FeedId, cancellationToken);
        var fromTrip = await ResolveReferenceAsync<Trip>(model.FromTripId,
            tripManager.EnsureExistsInScopeAsync, entity.FeedId, cancellationToken);
        var toTrip = await ResolveReferenceAsync<Trip>(model.ToTripId,
            tripManager.EnsureExistsInScopeAsync, entity.FeedId, cancellationToken);
        EnsureRouteMatchesTrip(fromRoute, fromTrip);
        EnsureRouteMatchesTrip(toRoute, toTrip);
        entity.FromRouteId = fromRoute?.Id;
        entity.ToRouteId = toRoute?.Id;
        entity.FromTripId = fromTrip?.Id;
        entity.ToTripId = toTrip?.Id;
        await EnsureLinkedServicesAsync(entity, fromTrip, toTrip, cancellationToken);
    }

    // Istege bagli GTFS bagini bulur ve ayni Feed bagini zorunlu tutar.
    private static async Task<T?> ResolveReferenceAsync<T>(
        Guid? id, Func<Guid, CancellationToken, Task<T>> get, Guid feedId,
        CancellationToken cancellationToken = default)
        where T : class, IGtfsEntity
    {
        if (id is null) return null;
        var value = await get(id.Value, cancellationToken);
        if (value.FeedId != feedId)
            throw new BusinessException(TransferExceptionCodes.ReferenceOutOfFeed);
        return value;
    }

    // Aktarma konumunun durak veya istasyon, bagli sefer konumunun durak olmasini saglar.
    private async Task EnsureStopTypeAsync(
        Stop? stop, int transferType, CancellationToken cancellationToken = default)
    {
        if (stop is null) return;
        var locationCode = stop.LocationTypeId is null
            ? LocationTypeConsts.StopOrPlatformCode
            : (await locationTypeRepository.GetAsync(
                stop.LocationTypeId.Value, cancellationToken: cancellationToken)).Code;
        if (locationCode == LocationTypeConsts.StopOrPlatformCode) return;
        if (locationCode == LocationTypeConsts.StationCode && !IsLinked(transferType)) return;
        throw new BusinessException(TransferExceptionCodes.StopTypeInvalid);
    }

    // Seferin secilen guzergah uzerinde calistigini dogrular.
    private static void EnsureRouteMatchesTrip(Route? route, Trip? trip)
    {
        if (route is not null && trip is not null && trip.RouteId != route.Id)
            throw new BusinessException(TransferExceptionCodes.RouteTripMismatch);
    }

    // Birden coga ve coktan bire bagli sefer kollarinin service_id degerini korur.
    private async Task EnsureLinkedServicesAsync(
        Transfer entity, Trip? fromTrip, Trip? toTrip, CancellationToken cancellationToken = default)
    {
        if (!IsLinked(entity.TransferType)) return;
        var fromTripId = entity.FromTripId;
        var toTripId = entity.ToTripId;
        var id = entity.Id;
        var others = await repository.GetListAsync(
            value => value.Id != id && (value.TransferType == TransferConsts.InSeatAllowedCode
                || value.TransferType == TransferConsts.InSeatForbiddenCode)
                && (value.FromTripId == fromTripId || value.ToTripId == toTripId),
            includeDetails: true, cancellationToken: cancellationToken);
        foreach (var other in others)
        {
            if (other.FromTripId == fromTripId && other.ToTrip!.ServiceId != toTrip!.ServiceId)
                throw new BusinessException(TransferExceptionCodes.LinkedServiceMismatch);
            if (other.ToTripId == toTripId && other.FromTrip!.ServiceId != fromTrip!.ServiceId)
                throw new BusinessException(TransferExceptionCodes.LinkedServiceMismatch);
        }
    }

    // GTFS birlesik aktarma anahtarinin ayni Feed'de tekrarini engeller.
    private Task CheckTransferUniqueAsync(Transfer entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var fromStopId = entity.FromStopId;
        var toStopId = entity.ToStopId;
        var fromTripId = entity.FromTripId;
        var toTripId = entity.ToTripId;
        var fromRouteId = entity.FromRouteId;
        var toRouteId = entity.ToRouteId;
        return CheckAlreadyExistsAsync(value => value.FeedId == feedId
            && value.FromStopId == fromStopId && value.ToStopId == toStopId
            && value.FromTripId == fromTripId && value.ToTripId == toTripId
            && value.FromRouteId == fromRouteId && value.ToRouteId == toRouteId,
            entity.Id, cancellationToken);
    }

    // Bagli sefer turlerini GTFS 4 ve 5 kodlarindan tanir.
    private static bool IsLinked(int transferType)
        => transferType is TransferConsts.InSeatAllowedCode or TransferConsts.InSeatForbiddenCode;
}

