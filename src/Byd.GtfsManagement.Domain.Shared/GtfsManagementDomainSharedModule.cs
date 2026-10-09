using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Byd.GtfsManagement.Localization;
using Byd.AuthManagement;
using Volo.Abp.Domain;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Byd.GtfsManagement;

// islevi: GTFS lokalizasyon kaynagini ve hata kodu ad alanini kaydeder.
// sistemdeki gorevi: Modulun sabitlerini ve lokalizasyon dosyalarini tum katmanlara acar.
[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpDddDomainSharedModule),
    typeof(AuthManagementDomainSharedModule)
)]
public class GtfsManagementDomainSharedModule : AbpModule
{
    // Modulun servis ve secenek kayitlarini yapar.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<GtfsManagementDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<GtfsManagementResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/GtfsManagement");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(GtfsManagementErrorCodes.RootNamespace, typeof(GtfsManagementResource));
        });
    }
}
