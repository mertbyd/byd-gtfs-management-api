using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Interfaces.Routes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Route repository sozlesmesini core EF tabanina baglar ve ajans ile lookup navigasyonlarini yukler.
// sistemdeki gorevi: Cevap DTO'sunun ajans ve lookup adlarini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(IRouteRepository))]
public class EfCoreRouteRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Route, Guid>(dbContextProvider), IRouteRepository
{
    // Tekil okumalarda ajans, ulasim turu ve yol boyu kurallarinin adlarinin dolu gelmesini saglar.
    public override async Task<IQueryable<Route>> WithDetailsAsync()
        => IncludeNames(await GetQueryableAsync());

    // Liste sorgusunda da ayni adlari yukler; filtre, siralama ve sayfalama tabanda kalir.
    protected override async Task<IQueryable<Route>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeNames(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin ad alanlarini besleyen navigasyonlari ekler.
    private static IQueryable<Route> IncludeNames(IQueryable<Route> query)
        => query
            .Include(route => route.Agency)
            .Include(route => route.RouteType)
            .Include(route => route.ContinuousPickup)
            .Include(route => route.ContinuousDropOff)
            .Include(route => route.Network)
            .Include(route => route.CemvSupport);
}
