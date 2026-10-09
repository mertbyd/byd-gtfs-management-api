using Byd.AuthManagement;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

// islevi: GTFS Domain katmaninin bagimliliklarini tanimlar.
// sistemdeki gorevi: Entity, Manager ve seeder'larin auth modulunun Domain katmaniyla calismasini saglar.
[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(GtfsManagementDomainSharedModule),
    typeof(AuthManagementDomainModule)
)]
public class GtfsManagementDomainModule : AbpModule
{

}
