using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Models.Elasticsearch;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Interfaces.Routes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Route repository sozlesmesini core EF tabanina baglar ve ajans ile lookup navigasyonlarini yukler.
// sistemdeki gorevi: Cevap DTO'sunun ajans ve lookup adlarini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(IRouteRepository))]
public class EfCoreRouteRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Route>(dbContextProvider), IRouteRepository
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

    // Duraklara ugrayan guzergahlari ajans ve lookup adlariyla, siralama alanina gore getirir.
    public virtual async Task<List<Route>> GetAtStopsAsync(
        IReadOnlyCollection<Guid> stopIds, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var routeIds = dbContext.Set<StopTime>()
            .Where(stopTime => stopTime.StopId != null && stopIds.Contains(stopTime.StopId.Value))
            .Select(stopTime => stopTime.Trip.RouteId);
        return await (await WithDetailsAsync())
            .Where(route => routeIds.Contains(route.Id))
            .OrderBy(route => route.SortOrder).ThenBy(route => route.ShortName).ThenBy(route => route.LongName)
            .ToListAsync(cancellationToken);
    }

    // Guzergah aramasini organizasyonun guzergah indeksinde calistirir.
    public virtual Task<ElasticsearchSearchResult<Route>> SearchAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty,
        CancellationToken cancellationToken = default)
        where TFiltering : class
        => SearchInIndexAsync(RouteConsts.SearchIndexName, pagingProperty, cancellationToken);

    // Organizasyonun guzergah indeksini Feed'in guzergahlariyla yeniden doldurur.
    public virtual Task ReplaceSearchIndexAsync(Guid feedId, CancellationToken cancellationToken = default)
        => ReplaceIndexAsync(RouteConsts.SearchIndexName, feedId, cancellationToken);
}
