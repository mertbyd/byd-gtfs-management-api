using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Models.Elasticsearch;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Entities.Routes;

namespace Byd.GtfsManagement.Interfaces.Routes;

// islevi: Route icin core repository sozlesmesini ve guzergah arama indeksi islemlerini bildirir.
// sistemdeki gorevi: Domain kurallarini EF ve Elasticsearch uygulamasindan ayirir.
public interface IRouteRepository : IExtendedRepository<Route>
{
    // Duraklara ugrayan guzergahlari ayrintilariyla, siralama alanina gore getirir.
    Task<List<Route>> GetAtStopsAsync(IReadOnlyCollection<Guid> stopIds, CancellationToken cancellationToken = default);

    // Filtreyi organizasyonun guzergah arama indeksinde calistirir; sayfayi ayrintili guzergah kayitlariyla doner.
    Task<ElasticsearchSearchResult<Route>> SearchAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty,
        CancellationToken cancellationToken = default)
        where TFiltering : class;

    // Organizasyonun guzergah arama indeksini verilen Feed'in guzergahlariyla yeniden doldurur.
    Task ReplaceSearchIndexAsync(Guid feedId, CancellationToken cancellationToken = default);
}
