using Localization.Resources.AbpUi;
using Byd.GtfsManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Byd.GtfsManagement;

// islevi: GTFS HTTP katmaninin controller'larini ve lokalizasyon tabanini kaydeder.
// sistemdeki gorevi: Yalniz bu modulun uclarini yayinlar; core, file, log ve auth uclari kendi host'larinda kalir.
[DependsOn(
    typeof(GtfsManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class GtfsManagementHttpApiModule : AbpModule
{
    // Diger moduller yapilandirilmadan once MVC uygulama parcasini ekler.
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(GtfsManagementHttpApiModule).Assembly);
        });
    }

    // Modulun servis ve secenek kayitlarini yapar.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<GtfsManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
