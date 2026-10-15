using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.FileManagement.Services.StoredFiles;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.GtfsMapper;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Byd.GtfsManagement.Executors;

/// <summary>
/// Dogrulanmis Feed'in GTFS dosyalarini ve Integration durumunu tek islemde kaydeder.
/// </summary>
// islevi: Olay handler'i ile periyodik iscinin ayni ice aktarma adimini calistirir.
// sistemdeki gorevi: Tenant kapsamindaki Validated Feed'i yeniden okur; yinelenen olaylari atlar ve hata halinde yazimi geri alir.
public class FeedIntegrationExecutor(
    FeedManager manager,
    IStoredFileAppService storedFiles,
    GtfsArchiveMapper mapper,
    IFeedRepository repository,
    IUnitOfWorkManager unitOfWorkManager,
    IIdentityUserRepository userRepository,
    INotificationLogger notificationLogger,
    IDataFilter<IMultiTenant> multiTenantFilter,
    ICurrentTenant currentTenant,
    ICurrentOrganization currentOrganization,
    IAbpDistributedLock distributedLock) : ITransientDependency
{
    // Olay ve worker icin ayni kapsam ve Feed kilidi altinda entegrasyonu baslatir.
    public virtual async Task IntegrateAsync(Guid feedId, Guid tenantId, Guid organizationId,
        CancellationToken cancellationToken = default)
    {
        using var tenantScope = currentTenant.Change(tenantId);
        using var organizationScope = currentOrganization.Change(organizationId);
        await using var feedLock = await distributedLock.TryAcquireAsync(
            FeedConsts.IntegrationLockName + feedId, cancellationToken: cancellationToken);
        if (feedLock is null)
            return;

        await IntegrateLockedAsync(feedId, cancellationToken);
    }

    // Dis dosya erisimini transaction disinda tutar; islem basinda durumu tekrar kontrol eder.
    private async Task IntegrateLockedAsync(Guid feedId, CancellationToken cancellationToken)
    {
        var feed = await manager.EnsureExistsIncludingInactiveInScopeAsync(feedId, cancellationToken);
        if (feed.Status != FeedStatus.Validated)
            return;
        var archive = await storedFiles.DownloadAsync(feed.StoredFileId, cancellationToken);
        await ImportArchiveAsync(feedId, archive.Data, cancellationToken);
    }
    // Dosya indirildikten sonra kuyrugu yeniden kontrol eder; tablolar ve Integrated damgasi tek transaction'da commit edilir.
    private async Task ImportArchiveAsync(Guid feedId, byte[] archive, CancellationToken cancellationToken)
    {
        using var unitOfWork = unitOfWorkManager.Begin(requiresNew: true, isTransactional: true);
        var feed = await manager.FindWaitingForIntegrationAsync(feedId, cancellationToken);
        if (feed is null)
            return;
        await mapper.ImportAsync(feed, archive, cancellationToken);
        manager.SetIntegrated(feed);
        await repository.UpdateAsync(feed, autoSave: true, cancellationToken: cancellationToken);
        await NotifyAsync(feed, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
    // Alıcı host veya tenant kapsamında kayıtlı olabilir; kullanıcı adını bulup Log modülüne bildirir.
    private async Task NotifyAsync(Feed feed, CancellationToken cancellationToken)
    {
        var recipientId = feed.LastOperationUserId ?? feed.CreatorId;
        if (recipientId is null)
            return;
        IdentityUser? user;
        using (multiTenantFilter.Disable())
            user = await userRepository.FindAsync(recipientId.Value, cancellationToken: cancellationToken);
        if (user is null)
            return;
        await notificationLogger.NotifyAsync(new NotificationRecord(
            [NotificationConsts.WebChannelCode], FeedStatus.Integrated.ToString(),
            new Dictionary<string, string>
            {
                [FeedConsts.TurkishLanguageCode] = feed.Name,
                [FeedConsts.EnglishLanguageCode] = feed.Name
            })
        {
            ExternalId = feed.Id,
            Recipients = [new NotificationRecipientEto
            {
                RecipientUserId = user.Id,
                RecipientUserName = user.UserName
            }]
        }, cancellationToken);
    }
}
