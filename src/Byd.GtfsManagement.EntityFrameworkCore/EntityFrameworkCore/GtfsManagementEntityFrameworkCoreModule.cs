using Microsoft.Extensions.DependencyInjection;
using Byd.AuthManagement.EntityFrameworkCore;
using Byd.FileManagement.EntityFrameworkCore;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.GtfsDownloader;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.LogManagement;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: GTFS EF Core katmanini, varsayilan repository'leri ve kaynak indiricisini kaydeder.
// sistemdeki gorevi: Modulun kalicilik katmanini auth ve file modullerinin EF katmanlariyla birlestirir.
[DependsOn(
    typeof(GtfsManagementDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AuthManagementEntityFrameworkCoreModule),
    typeof(FileManagementEntityFrameworkCoreModule),
    typeof(LogManagementApplicationModule)
)]
public class GtfsManagementEntityFrameworkCoreModule : AbpModule
{
    // Modulun servis ve secenek kayitlarini yapar.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<GtfsManagementDbContext>(options => { options.AddDefaultRepositories(true); });
        context.Services.AddTransient<IGtfsFeedDownloader, GtfsFeedDownloader>();
        context.Services.AddHttpClient(FeedConsts.HttpClientName, client =>
            {
                client.Timeout = FeedConsts.RequestTimeout;
                client.MaxResponseContentBufferSize = FeedConsts.MaxArchiveBytes;
            })
            .RemoveAllLoggers();
    }
}
