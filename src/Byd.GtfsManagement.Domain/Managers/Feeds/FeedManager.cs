using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Managers;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.Feeds;
using Volo.Abp;
using Volo.Abp.Data;

namespace Byd.GtfsManagement.Managers.Feeds;

// islevi: Feed kapsam kuralini yonetir.
// sistemdeki gorevi: GTFS kayitlarinin bagli oldugu Feed'in istegin tenant'inda olmasini garanti eder.
public class FeedManager(
    IFeedRepository repository,
    IDataFilter dataFilter)
    : BaseManager<Feed, Guid>(repository)
{
    // Yalnizca istegin tenant'indaki Feed'i getirir.
    public virtual async Task<Feed> EnsureExistsInScopeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        EnsureScopeAvailable();
        var tenantId = CurrentTenant.Id;
        return (await TryGetByAsync(
            feed => feed.Id == id && feed.TenantId == tenantId,
            cancellationToken: cancellationToken))!;
    }

    // Pasif Feed'e bagli GTFS kayitlari da duzenlenebildigi icin aktiflik filtresini kapatir.
    public virtual async Task<Feed> EnsureExistsIncludingInactiveInScopeAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        using var inactiveIncluded = dataFilter.Disable<IHasIsActive>();
        return await EnsureExistsInScopeAsync(id, cancellationToken);
    }

    // Istegin tenant'indaki aktif Feed'i getirir; aktif Feed yoksa null doner.
    public virtual Task<Feed?> FindActiveAsync(CancellationToken cancellationToken = default)
    {
        EnsureScopeAvailable();
        var tenantId = CurrentTenant.Id;
        return Repository.FindAsync(feed => feed.TenantId == tenantId && feed.IsActive, cancellationToken: cancellationToken);
    }

    // Tenant'siz host isteklerinin Feed verisine erisimini reddeder.
    private void EnsureScopeAvailable()
    {
        if (CurrentTenant.Id is null)
            throw new BusinessException(FeedExceptionCodes.ScopeRequired);
    }
}
