using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Managers;
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
    where TEntity : class, IEntity<Guid>, IHasFeed
{
    // Hedef Feed'i kapsamda dogrulayip kaydin GTFS alanlarini kuran alt sinif akisi.
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

    // Baska GTFS kayitlari tarafindan kullanilan satirin silinmesini engeller.
    public virtual async Task EnsureCanDeleteAsync(
        TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (await Repository.IsDataUsedAsync(entity, ignoredEntityTypes: null, cancellationToken))
            throw new BusinessException(GeneralExceptionCodes.RelationalDataDelete);
    }

    // Verilen Feed'i kapsamda, verilmezse tenant'in aktif Feed'ini getirir; aktif Feed yoksa null doner.
    protected async Task<Feed?> ResolveReadFeedAsync(Guid? feedId, CancellationToken cancellationToken = default)
        => feedId is null
            ? await feedManager.FindActiveAsync(cancellationToken)
            : await feedManager.EnsureExistsIncludingInactiveInScopeAsync(feedId.Value, cancellationToken);

    // Kaydin kapsamini devralacagi Feed'i getirir ve istegin kapsaminda oldugunu dogrular.
    protected Task<Feed> ResolveFeedAsync(Guid feedId, CancellationToken cancellationToken = default)
        => feedManager.EnsureExistsIncludingInactiveInScopeAsync(feedId, cancellationToken);
}
