using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

[DependsOn(
    typeof(GtfsManagementDomainModule),
    typeof(GtfsManagementTestBaseModule)
)]
public class GtfsManagementDomainTestModule : AbpModule
{

}
