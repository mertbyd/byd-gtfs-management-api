using Microsoft.Extensions.DependencyInjection;
using Byd.AuthManagement.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement.EntityFrameworkCore;

[DependsOn(
    typeof(GtfsManagementDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AuthManagementEntityFrameworkCoreModule)
)]
public class GtfsManagementEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<GtfsManagementDbContext>(options => { options.AddDefaultRepositories(true); });
    }
}
