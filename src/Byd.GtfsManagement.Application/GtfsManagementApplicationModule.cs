using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Byd.GtfsManagement.Workers.Feeds;
using Byd.LogManagement;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

// islevi: GTFS Application katmaninin bagimliliklarini, Mapperly'yi, dogrulayiciyi ve iscileri kaydeder.
// sistemdeki gorevi: Modulun use case katmanini file ve log modullerinin Application katmanlariyla birlestirir. Auth'un
// Application katmani token sunucusunu tasidigi icin yerine log'un Application katmani baglanir.
[DependsOn(
    typeof(GtfsManagementDomainModule),
    typeof(GtfsManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule),
    typeof(LogManagementApplicationModule)
    )]
public class GtfsManagementApplicationModule : AbpModule
{
    // Mapperly'yi ve yerel GTFS dogrulayicisini kaydeder.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<GtfsManagementApplicationModule>();
        context.Services.AddGtfsFeedIntegrations();
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<FeedIntegrationWorker>();
    }
}
