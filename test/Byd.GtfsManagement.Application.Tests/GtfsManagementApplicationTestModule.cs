using Volo.Abp.Modularity;

namespace Byd.GtfsManagement;

[DependsOn(
    typeof(GtfsManagementApplicationModule),
    typeof(GtfsManagementDomainTestModule)
    )]
public class GtfsManagementApplicationTestModule : AbpModule
{

}
