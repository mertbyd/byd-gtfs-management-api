using Microsoft.Extensions.DependencyInjection;
using Byd.LogManagement;
using Volo.Abp.Application;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

// islevi: GTFS Application katmaninin bagimliliklarini ve Mapperly'yi kaydeder.
// sistemdeki gorevi: Modulun use case katmanini log modulunun Application katmaniyla birlestirir. Auth'un Application
// katmani token sunucusunu tasidigi icin yerine log'un Application katmani baglanir.
[DependsOn(
    typeof(GtfsManagementDomainModule),
    typeof(GtfsManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule),
    typeof(LogManagementApplicationModule)
    )]
public class GtfsManagementApplicationModule : AbpModule
{
    // Mapperly nesne eslemesini kaydeder.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<GtfsManagementApplicationModule>();
    }
}
