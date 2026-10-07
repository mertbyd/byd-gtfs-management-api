using Byd.AuthManagement;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(GtfsManagementDomainSharedModule),
    typeof(AuthManagementDomainModule)
)]
public class GtfsManagementDomainModule : AbpModule
{

}
