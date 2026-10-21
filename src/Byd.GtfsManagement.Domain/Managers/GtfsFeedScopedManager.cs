using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Managers;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Managers.Feeds;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Managers;

// islevi: Feed kapsamli GTFS kayitlarinin kapsam ve silme guardlarini tek yerde yurutur.
// sistemdeki gorevi: Her GTFS Manager'inin turedigi taban; kapsam kararini FeedManager'a devreder ve alt
// siniflara yalnizca kendi GTFS kurallarini yazdirir.
public abstract class GtfsFeedScopedManager<TEntity, TCreateModel, TUpdateModel>(
    IExtendedRepository<TEntity> repository,
    FeedManager feedManager)
    : BaseManager<TEntity, Guid>(repository), IGtfsScopedManager<TEntity>
    where TEntity : class, IGtfsEntity
{
    // Kaydi organizasyonun aktif Feed'inde kurup GTFS alanlarini yazan alt sinif akisi.
    public abstract Task<TEntity> CreateAsync(TCreateModel model, CancellationToken cancellationToken = default);

    // Yuklenmis kaydin duzenlenebilir alanlarini ayni kurallardan geciren alt sinif akisi.
    public abstract Task UpdateAsync(
        TEntity entity, TUpdateModel model, CancellationToken cancellationToken = default);

    // Kaydi getirip bagli Feed'in istegin kapsaminda oldugunu dogrular.
    public virtual async Task<TEntity> EnsureExistsInScopeAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var entity = (await TryGetByIdAsync(id, cancellationToken: cancellationToken))!;
        await feedManager.EnsureExistsIncludingInactiveInScopeAsync(entity.FeedId, cancellationToken);
        return entity;
    }

    // Yonetim kaydi acikca istenen surum disindaysa goruntulenemez.
    public virtual void EnsureInFeed(TEntity entity, Guid feedId)
    {
        if (entity.FeedId != feedId)
            throw new BusinessException(GeneralExceptionCodes.EntityNotFound);
    }

    // Liste filtresindeki surum alanini sunucunun dogruladigi kapsama kilitler.
    public virtual void ScopeFilter(IFeedFilter filter, Guid feedId)
        => filter.FeedId = new DataTableFilterItem
        {
            Operator = DataTableFilterOperator.Equal,
            Value = feedId.ToString()
        };

    // Baska GTFS kayitlari tarafindan kullanilan satirin silinmesini engeller.
    public virtual async Task EnsureCanDeleteAsync(
        TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (await Repository.IsDataUsedAsync(entity, ignoredEntityTypes: null, cancellationToken))
            throw new BusinessException(GeneralExceptionCodes.RelationalDataDelete);
    }

    // Acikca istenen Feed'i getirir ve istegin kapsaminda oldugunu dogrular.
    protected Task<Feed> ResolveFeedAsync(Guid feedId, CancellationToken cancellationToken = default)
        => feedManager.EnsureExistsIncludingInactiveInScopeAsync(feedId, cancellationToken);

    // Yeni kaydin yazilacagi organizasyonun aktif Feed'ini getirir.
    protected Task<Feed> ResolveActiveFeedAsync(CancellationToken cancellationToken = default)
        => feedManager.GetActiveInScopeAsync(cancellationToken);
}
