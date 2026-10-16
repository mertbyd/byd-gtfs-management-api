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
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Interfaces.Stops;
using Byd.GtfsManagement.Models.Stops;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Stop repository sozlesmesini core EF tabanina baglar ve lookup ile ust istasyon navigasyonlarini yukler.
// sistemdeki gorevi: Cevap DTO'sunun lookup ve istasyon adlarini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(IStopRepository))]
public class EfCoreStopRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Stop>(dbContextProvider), IStopRepository
{
    // Tekil okumalarda konum turu, erisim durumu ve ust istasyon adlarinin dolu gelmesini saglar.
    public override async Task<IQueryable<Stop>> WithDetailsAsync()
        => IncludeNames(await GetQueryableAsync());

    // Liste sorgusunda da ayni adlari yukler; filtre, siralama ve sayfalama tabanda kalir.
    protected override async Task<IQueryable<Stop>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeNames(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin ad alanlarini besleyen navigasyonlari ekler.
    private static IQueryable<Stop> IncludeNames(IQueryable<Stop> query)
        => query
            .Include(stop => stop.LocationType)
            .Include(stop => stop.WheelchairBoarding)
            .Include(stop => stop.ParentStation)
            .Include(stop => stop.Level)
            .Include(stop => stop.StopAccess);

    // Feed'in tum duraklarini ust istasyon bagi, konum turu kodu ve koordinatlariyla planlayici modeline yansitir.
    public virtual async Task<List<StopPlaceModel>> GetPlacesAsync(Guid? feedId, CancellationToken cancellationToken = default)
        => await (await GetQueryableAsync())
            .Where(stop => (feedId == null || stop.FeedId == feedId))
            .Select(stop => new StopPlaceModel
            {
                Id = stop.Id,
                ParentStationId = stop.ParentStationId,
                LocationTypeCode = stop.LocationType == null ? LocationTypeConsts.StopOrPlatformCode : stop.LocationType.Code,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude
            })
            .ToListAsync(cancellationToken);

    // Durak aramasini organizasyonun durak indeksinde calistirir.
    public virtual Task<ElasticsearchSearchResult<Stop>> SearchAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty,
        CancellationToken cancellationToken = default)
        where TFiltering : class
        => SearchInIndexAsync(StopConsts.SearchIndexName, pagingProperty, cancellationToken);

    // Organizasyonun durak indeksini Feed'in duraklariyla yeniden doldurur.
    public virtual Task ReplaceSearchIndexAsync(Guid feedId, CancellationToken cancellationToken = default)
        => ReplaceIndexAsync(StopConsts.SearchIndexName, feedId, cancellationToken);
}
