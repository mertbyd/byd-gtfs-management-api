using Byd.AuthManagement;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

// islevi: GTFS Domain katmaninin bagimliliklarini tanimlar.
// sistemdeki gorevi: Entity, Manager ve seeder'larin auth ve file modullerinin Domain katmanlariyla calismasini saglar.
[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(GtfsManagementDomainSharedModule),
    typeof(AuthManagementDomainModule)

)]
public class GtfsManagementDomainModule : AbpModule
{
    // Normal GTFS okumalari yayinlanmis surumlerle baslar; taslak islemleri kapsami koruyarak kapatir.
    public override void ConfigureServices(ServiceConfigurationContext context)
        => Configure<AbpDataFilterOptions>(options =>
            options.DefaultStates[typeof(ActiveFeedFilter)] = new DataFilterState(true));
}
