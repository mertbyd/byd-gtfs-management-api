using Byd.AuthManagement;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Byd.GtfsManagement;

[DependsOn(
    typeof(GtfsManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AuthManagementApplicationContractsModule)
    )]
public class GtfsManagementApplicationContractsModule : AbpModule
{

}
