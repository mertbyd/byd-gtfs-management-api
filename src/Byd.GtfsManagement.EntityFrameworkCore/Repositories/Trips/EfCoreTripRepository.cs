using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces.Trips;
using Byd.GtfsManagement.Models.Routes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Trip repository sozlesmesini core EF tabanina baglar ve guzergah, servis ile lookup navigasyonlarini yukler.
// sistemdeki gorevi: Cevap DTO'sunun guzergah, servis ve lookup adlarini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(ITripRepository))]
public class EfCoreTripRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Trip, Guid>(dbContextProvider), ITripRepository
{
    // Tekil okumalarda guzergah, servis ve lookup adlarinin dolu gelmesini saglar.
    public override async Task<IQueryable<Trip>> WithDetailsAsync()
        => IncludeNames(await GetQueryableAsync());

    // Liste sorgusunda da ayni adlari yukler; filtre, siralama ve sayfalama tabanda kalir.
    protected override async Task<IQueryable<Trip>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeNames(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin ad alanlarini besleyen navigasyonlari ekler.
    private static IQueryable<Trip> IncludeNames(IQueryable<Trip> query)
        => query
            .Include(trip => trip.Route)
            .Include(trip => trip.Service)
            .Include(trip => trip.TripDirection)
            .Include(trip => trip.WheelchairAccessible)
            .Include(trip => trip.BikesAllowed)
            .Include(trip => trip.CarsAllowed);

    // Guzergahin seferlerini yon ve tabelaya gore gruplar; her kalibin ornegi en kucuk trip_id'li seferdir ve ayrintilariyla doner.
    public virtual async Task<List<RoutePatternModel>> GetPatternsAsync(Guid routeId, CancellationToken cancellationToken = default)
    {
        var groups = await (await GetQueryableAsync())
            .Where(trip => trip.RouteId == routeId)
            .GroupBy(trip => new { trip.TripDirectionId, trip.Headsign })
            .Select(group => new { Count = group.Count(), GtfsTripId = group.Min(trip => trip.GtfsTripId)! })
            .ToListAsync(cancellationToken);
        var gtfsTripIds = groups.Select(group => group.GtfsTripId).ToList();
        var representatives = await (await WithDetailsAsync())
            .Where(trip => trip.RouteId == routeId && gtfsTripIds.Contains(trip.GtfsTripId))
            .ToDictionaryAsync(trip => trip.GtfsTripId, cancellationToken);
        return groups.OrderByDescending(group => group.Count)
            .Select(group => new RoutePatternModel { Trip = representatives[group.GtfsTripId], TripCount = group.Count })
            .ToList();
    }
}
