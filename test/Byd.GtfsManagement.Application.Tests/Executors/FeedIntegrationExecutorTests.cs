using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Byd.CoreExtensions.Interfaces;
using Byd.FileManagement.Dtos.StoredFiles;
using Byd.FileManagement.Services.StoredFiles;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.GtfsMapper;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Services;
using Byd.GtfsManagement.Executors;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using Xunit;

namespace Byd.GtfsManagement.Executors;

public class FeedIntegrationExecutorTests
{
    [Fact]
    public async Task IntegrateAsync_WhenFeedLockIsBusy_SkipsImport()
    {
        var feedId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();
        var tenant = Substitute.For<ICurrentTenant>();
        var organization = Substitute.For<ICurrentOrganization>();
        tenant.Change(tenantId).Returns(Substitute.For<IDisposable>());
        organization.Change(organizationId).Returns(Substitute.For<IDisposable>());
        var distributedLock = Substitute.For<IAbpDistributedLock>();
        distributedLock.TryAcquireAsync(FeedConsts.IntegrationLockName + feedId,
                Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IAbpDistributedLockHandle?>(null));
        var manager = CreateManager(Substitute.For<IFeedRepository>());
        var storedFiles = Substitute.For<IStoredFileAppService>();
        var executor = CreateExecutor(manager, storedFiles, null!, null!, null!, tenant, organization, distributedLock);

        await executor.IntegrateAsync(feedId, tenantId, organizationId);

        await manager.DidNotReceive().EnsureExistsIncludingInactiveInScopeAsync(feedId, Arg.Any<CancellationToken>());
        await storedFiles.DidNotReceive().DownloadAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task IntegrateAsync_WhenAlreadyIntegrated_DoesNotNotifyAgain()
    {
        var tenantId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();
        var feed = new Feed(Guid.NewGuid(), tenantId, organizationId, "Vilnius", FeedSourceType.Upload, null);
        typeof(Feed).GetProperty(nameof(Feed.Status))!.SetValue(feed, FeedStatus.Integrated);
        var manager = CreateManager(Substitute.For<IFeedRepository>());
        manager.EnsureExistsIncludingInactiveInScopeAsync(feed.Id, Arg.Any<CancellationToken>()).Returns(feed);
        var tenant = Substitute.For<ICurrentTenant>();
        var organization = Substitute.For<ICurrentOrganization>();
        tenant.Change(tenantId).Returns(Substitute.For<IDisposable>());
        organization.Change(organizationId).Returns(Substitute.For<IDisposable>());
        var distributedLock = Substitute.For<IAbpDistributedLock>();
        distributedLock.TryAcquireAsync(FeedConsts.IntegrationLockName + feed.Id,
                Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IAbpDistributedLockHandle?>(Substitute.For<IAbpDistributedLockHandle>()));
        var storedFiles = Substitute.For<IStoredFileAppService>();
        var notificationLogger = Substitute.For<INotificationLogger>();
        var executor = new FeedIntegrationExecutor(manager, storedFiles, null!, null!, null!,
            Substitute.For<IIdentityUserRepository>(), notificationLogger,
            Substitute.For<IDataFilter<IMultiTenant>>(), tenant, organization, distributedLock);

        await executor.IntegrateAsync(feed.Id, tenantId, organizationId);

        await storedFiles.DidNotReceive().DownloadAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await notificationLogger.DidNotReceiveWithAnyArgs().NotifyAsync(default!, default);
    }

    [Fact]
    public async Task IntegrateAsync_WhenTableImportFails_LeavesFeedValidated()
    {
        var tenantId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();
        var feed = new Feed(Guid.NewGuid(), tenantId, organizationId, "Vilnius", FeedSourceType.Upload, null);
        typeof(Feed).GetProperty(nameof(Feed.Status))!.SetValue(feed, FeedStatus.Validated);
        var repository = Substitute.For<IFeedRepository>();
        var manager = CreateManager(repository);
        manager.EnsureExistsIncludingInactiveInScopeAsync(feed.Id, Arg.Any<CancellationToken>()).Returns(feed);
        manager.FindWaitingForIntegrationAsync(feed.Id, Arg.Any<CancellationToken>()).Returns(feed);
        var storedFiles = Substitute.For<IStoredFileAppService>();
        storedFiles.DownloadAsync(feed.StoredFileId, Arg.Any<CancellationToken>())
            .Returns(new DownloadStoredFileDto { Data = [1, 2, 3] });
        var mapper = Substitute.For<GtfsArchiveMapper>(
            Array.Empty<IGtfsImportExportAppService<GtfsArchiveContext>>(), repository);
        mapper.ImportAsync(feed, Arg.Any<byte[]>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromException(new InvalidOperationException("database unavailable")));
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
        unitOfWorkManager.Begin(Arg.Any<AbpUnitOfWorkOptions>(), Arg.Any<bool>()).Returns(unitOfWork);
        var tenant = Substitute.For<ICurrentTenant>();
        var organization = Substitute.For<ICurrentOrganization>();
        tenant.Change(tenantId).Returns(Substitute.For<IDisposable>());
        organization.Change(organizationId).Returns(Substitute.For<IDisposable>());
        var distributedLock = Substitute.For<IAbpDistributedLock>();
        var handle = Substitute.For<IAbpDistributedLockHandle>();
        distributedLock.TryAcquireAsync(FeedConsts.IntegrationLockName + feed.Id,
                Arg.Any<TimeSpan>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IAbpDistributedLockHandle?>(handle));
        var executor = CreateExecutor(manager, storedFiles, mapper, repository, unitOfWorkManager,
            tenant, organization, distributedLock);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            executor.IntegrateAsync(feed.Id, tenantId, organizationId));

        Assert.Equal(FeedStatus.Validated, feed.Status);
        manager.DidNotReceive().SetIntegrated(Arg.Any<Feed>());
        await unitOfWork.DidNotReceive().CompleteAsync(Arg.Any<CancellationToken>());
        await handle.Received(1).DisposeAsync();
    }

    private static FeedManager CreateManager(IFeedRepository repository)
        => Substitute.For<FeedManager>(repository, Substitute.For<IDataFilter>(),
            null!, Substitute.For<ICurrentOrganization>(), Substitute.For<ICurrentUser>());

    private static FeedIntegrationExecutor CreateExecutor(FeedManager manager,
        IStoredFileAppService storedFiles, GtfsArchiveMapper mapper, IFeedRepository repository,
        IUnitOfWorkManager unitOfWorkManager, ICurrentTenant tenant,
        ICurrentOrganization organization, IAbpDistributedLock distributedLock)
        => new(manager, storedFiles, mapper, repository, unitOfWorkManager,
            Substitute.For<IIdentityUserRepository>(), Substitute.For<INotificationLogger>(),
            Substitute.For<IDataFilter<IMultiTenant>>(), tenant, organization, distributedLock);
}
