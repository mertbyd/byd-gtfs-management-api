using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Models.Elasticsearch;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Models.Stops;

namespace Byd.GtfsManagement.Interfaces.Stops;

// islevi: Stop icin core repository sozlesmesini ve durak arama indeksi islemlerini bildirir.
// sistemdeki gorevi: Domain kurallarini EF ve Elasticsearch uygulamasindan ayirir.
public interface IStopRepository : IExtendedRepository<Stop>
{
    // Feed'in tum duraklarini yolculuk planlayicisi icin hafif modelle getirir.
    Task<List<StopPlaceModel>> GetPlacesAsync(Guid? feedId, CancellationToken cancellationToken = default);

    // Filtreyi organizasyonun durak arama indeksinde calistirir; sayfayi ayrintili durak kayitlariyla doner.
    Task<ElasticsearchSearchResult<Stop>> SearchAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty,
        CancellationToken cancellationToken = default)
        where TFiltering : class;

    // Organizasyonun durak arama indeksini verilen Feed'in duraklariyla yeniden doldurur.
    Task ReplaceSearchIndexAsync(Guid feedId, CancellationToken cancellationToken = default);
}
