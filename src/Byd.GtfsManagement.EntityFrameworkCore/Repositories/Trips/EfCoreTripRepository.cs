using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces.Trips;
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
            .Include(trip => trip.BikesAllowed);
}
