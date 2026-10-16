using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Models.Elasticsearch;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: GTFS kayit sorgularina surum adini besleyen ortak navigation'i ekler; arama indeksi olan kayitlar icin
// organizasyonun indeksinde aramayi ve indeksi bir Feed'in kayitlariyla yeniden doldurmayi saglar.
// sistemdeki gorevi: Core repository davranisini devralir; GTFS'e ozel surum cevabi ve arama akisi repository'lerde
// tekrarlanmaz.
public abstract class EfGtfsRepository<TEntity>(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, TEntity, Guid>(dbContextProvider)
    where TEntity : class, IGtfsEntity
{
    private IElasticsearchRepository<TEntity, Guid> SearchRepository =>
        LazyServiceProvider.LazyGetRequiredService<IElasticsearchRepository<TEntity, Guid>>();

    private ICurrentOrganization CurrentOrganization => LazyServiceProvider.LazyGetRequiredService<ICurrentOrganization>();

    // Entity cevaplari surum adi tasir; projeksiyon ve sayimlarda EF gereksiz Include'u kaldirir.
    public override async Task<IQueryable<TEntity>> GetQueryableAsync()
        => (await base.GetQueryableAsync()).Include(entity => entity.Feed);

    // Filtreyi organizasyonun arama indeksinde calistirir; sayfadaki kayitlari veritabanindan ayrintilariyla, indeksin
    // dondugu sirayla verir. Organizasyonun henuz indeksi yoksa bos sonuc doner.
    protected virtual async Task<ElasticsearchSearchResult<TEntity>> SearchInIndexAsync<TFiltering>(
        string indexName,
        IDataTablePagingProperty<TFiltering> pagingProperty,
        CancellationToken cancellationToken = default)
        where TFiltering : class
    {
        var index = ToOrganizationIndex(indexName);
        if (!await SearchRepository.IndexExistsAsync(index, cancellationToken))
            return new ElasticsearchSearchResult<TEntity>();
        var result = await SearchRepository.SearchAsync(pagingProperty, index, cancellationToken);
        var ids = result.Items.Select(entity => entity.Id).ToList();
        var entities = await (await WithDetailsAsync())
            .Where(entity => ids.Contains(entity.Id))
            .ToListAsync(cancellationToken);
        result.Items = ids.Join(entities, id => id, entity => entity.Id, (_, entity) => entity).ToList();
        return result;
    }

    // Organizasyonun arama indeksini bosaltip verilen Feed'in kayitlarini navigation'siz yazar.
    protected virtual async Task ReplaceIndexAsync(
        string indexName,
        Guid feedId,
        CancellationToken cancellationToken = default)
    {
        var index = ToOrganizationIndex(indexName);
        var entities = await (await GetDbSetAsync())
            .AsNoTracking()
            .Where(entity => entity.FeedId == feedId)
            .ToListAsync(cancellationToken);
        await SearchRepository.ClearIndexAsync(index, cancellationToken);
        if (entities.Count > 0)
            await SearchRepository.InsertManyAsync(entities, index, cancellationToken);
    }

    // Indeks adina yururlukteki organizasyonu ekler.
    private string ToOrganizationIndex(string indexName)
        => $"{indexName}{DatabaseConsts.SearchIndexOrganizationSeparator}{CurrentOrganization.Id}";
}
