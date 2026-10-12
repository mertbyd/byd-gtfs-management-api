using Localization.Resources.AbpUi;
using Byd.GtfsManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Byd.CoreExtensions;

namespace Byd.GtfsManagement;

[DependsOn(
    typeof(GtfsManagementApplicationContractsModule),
    typeof(CoreExtensionsHttpApiModule),
    typeof(AbpAspNetCoreMvcModule))]
public class GtfsManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(GtfsManagementHttpApiModule).Assembly);
        });
    }

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
