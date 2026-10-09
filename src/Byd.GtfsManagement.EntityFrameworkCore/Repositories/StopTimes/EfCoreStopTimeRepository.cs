using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Models.StopTimes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: StopTime repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Cevap DTO'sunun sefer, durak ve lookup adlarini yukler.
/// <summary>StopTime EF sorgularini uygular.</summary>
[ExposeServices(typeof(IStopTimeRepository))]
public class EfCoreStopTimeRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, StopTime, Guid>(dbContextProvider), IStopTimeRepository
{
    // Tek kayit okumalarinda sefer, durak ve lookup adlarini yukler.
    public override async Task<IQueryable<StopTime>> WithDetailsAsync()
        => IncludeNames(await GetQueryableAsync());

    // Filtreli listede ayni iliskili adlari sorguya ekler.
    protected override async Task<IQueryable<StopTime>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeNames(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin kullandigi navigation alanlarini yukler.
    private static IQueryable<StopTime> IncludeNames(IQueryable<StopTime> query)
        => query
            .Include(value => value.Trip)
            .Include(value => value.Stop)
            .Include(value => value.LocationGroup)
            .Include(value => value.PickupType)
            .Include(value => value.DropOffType)
            .Include(value => value.ContinuousPickup)
            .Include(value => value.ContinuousDropOff);

    // Duraklardan verilen servislerle ve saatten sonra kalkan ziyaretleri kalkis sirasiyla, sefer ve guzergahiyla getirir.
    public virtual async Task<List<StopTime>> GetDeparturesAsync(
        IReadOnlyCollection<Guid> stopIds, IReadOnlyCollection<Guid> serviceIds, string fromTime, int limit,
        CancellationToken cancellationToken = default)
        => await (await WithDetailsAsync())
            .Include(stopTime => stopTime.Trip).ThenInclude(trip => trip.Route)
            .Where(stopTime => stopTime.StopId != null && stopIds.Contains(stopTime.StopId.Value)
                && stopTime.DepartureTime != null && serviceIds.Contains(stopTime.Trip.ServiceId))
            .Where(stopTime => string.Compare(
                stopTime.DepartureTime!.Length == 7 ? "0" + stopTime.DepartureTime : stopTime.DepartureTime, fromTime) >= 0)
            .OrderBy(stopTime => stopTime.DepartureTime!.Length == 7 ? "0" + stopTime.DepartureTime : stopTime.DepartureTime)
            .Take(limit)
            .ToListAsync(cancellationToken);

    // Seferin durak ziyaretlerini durak ve lookup adlariyla sira numarasina gore getirir.
    public virtual async Task<List<StopTime>> GetTripStopsAsync(Guid tripId, CancellationToken cancellationToken = default)
        => await (await WithDetailsAsync())
            .Where(stopTime => stopTime.TripId == tripId)
            .OrderBy(stopTime => stopTime.StopSequence)
            .ToListAsync(cancellationToken);

    // Verilen servislerin saat araliginda kalkan veya varan, duraga bagli ve saatli ziyaretlerini getirir.
    public virtual async Task<List<StopVisitModel>> GetVisitsAsync(
        Guid feedId, IReadOnlyCollection<Guid> serviceIds, string fromTime, string toTime,
        CancellationToken cancellationToken = default)
        => await (await GetQueryableAsync())
            .Where(stopTime => stopTime.FeedId == feedId && stopTime.StopId != null
                && stopTime.ArrivalTime != null && stopTime.DepartureTime != null
                && serviceIds.Contains(stopTime.Trip.ServiceId))
            .Where(stopTime =>
                string.Compare(stopTime.DepartureTime!.Length == 7 ? "0" + stopTime.DepartureTime : stopTime.DepartureTime,
                    fromTime) >= 0
                && string.Compare(stopTime.DepartureTime!.Length == 7 ? "0" + stopTime.DepartureTime : stopTime.DepartureTime,
                    toTime) <= 0
                || string.Compare(stopTime.ArrivalTime!.Length == 7 ? "0" + stopTime.ArrivalTime : stopTime.ArrivalTime,
                    fromTime) >= 0
                && string.Compare(stopTime.ArrivalTime!.Length == 7 ? "0" + stopTime.ArrivalTime : stopTime.ArrivalTime,
                    toTime) <= 0)
            .Select(stopTime => new StopVisitModel
            {
                TripId = stopTime.TripId,
                StopId = stopTime.StopId!.Value,
                StopSequence = stopTime.StopSequence,
                ArrivalTime = stopTime.ArrivalTime!,
                DepartureTime = stopTime.DepartureTime!
            })
            .ToListAsync(cancellationToken);
}
