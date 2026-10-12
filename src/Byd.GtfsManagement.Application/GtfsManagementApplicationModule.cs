using Microsoft.Extensions.DependencyInjection;
using Byd.LogManagement;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Byd.GtfsManagement;

// Auth'un Application katmani token sunucusunu tasidigi icin yerine log'un Application katmani baglanir.
[DependsOn(
    typeof(GtfsManagementDomainModule),
    typeof(GtfsManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule),
    typeof(LogManagementApplicationModule)
    )]
public class GtfsManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<GtfsManagementApplicationModule>();
    }
}
