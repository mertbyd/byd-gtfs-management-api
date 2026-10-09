using Microsoft.Extensions.DependencyInjection;
using Byd.AuthManagement.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: GTFS EF Core katmanini ve varsayilan repository'leri kaydeder.
// sistemdeki gorevi: Modulun kalicilik katmanini auth modulunun EF katmaniyla birlestirir.
[DependsOn(
    typeof(GtfsManagementDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AuthManagementEntityFrameworkCoreModule)
)]
public class GtfsManagementEntityFrameworkCoreModule : AbpModule
{
    // Modulun servis ve secenek kayitlarini yapar.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<GtfsManagementDbContext>(options => { options.AddDefaultRepositories(true); });
    }
}
