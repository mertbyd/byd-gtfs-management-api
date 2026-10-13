using Byd.AuthManagement;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Byd.GtfsManagement;

// islevi: GTFS sozlesme katmaninin bagimliliklarini tanimlar.
// sistemdeki gorevi: DTO, servis arayuzu, dogrulayici ve izin tanimlarini tasiyan katmanin modul sinifidir.
[DependsOn(
    typeof(GtfsManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AuthManagementApplicationContractsModule)
    )]
public class GtfsManagementApplicationContractsModule : AbpModule
{

}
