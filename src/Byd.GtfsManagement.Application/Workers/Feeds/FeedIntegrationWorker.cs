using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Executors;
using Byd.GtfsManagement.Managers.Feeds;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Threading;

namespace Byd.GtfsManagement.Workers.Feeds;

/// <summary>Olayla tamamlanamayan Validated Feed'leri yeniden entegre eder.</summary>
public class FeedIntegrationWorker : AsyncPeriodicBackgroundWorkerBase
{
    public FeedIntegrationWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory)
        : base(timer, serviceScopeFactory)
    {
        Timer.Period = (int)TimeSpan.FromSeconds(FeedConsts.IntegrationScanPeriodSeconds).TotalMilliseconds;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var services = workerContext.ServiceProvider;
        var cancellationToken = workerContext.CancellationToken;
        await using var scanLock = await services.GetRequiredService<IAbpDistributedLock>()
            .TryAcquireAsync(FeedConsts.IntegrationLockName, cancellationToken: cancellationToken);
        if (scanLock is null)
            return;
        var feeds = await services.GetRequiredService<FeedManager>().GetWaitingForIntegrationAsync(cancellationToken);
        var executor = services.GetRequiredService<FeedIntegrationExecutor>();
        foreach (var feed in feeds)
            await executor.IntegrateAsync(
                feed.Id, feed.TenantId!.Value, feed.OrganizationId!.Value, cancellationToken);
    }
}
