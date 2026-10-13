using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Byd.GtfsManagement;

// islevi: GTFS sozlesmeleri icin HTTP istemci proxy'lerini kaydeder.
// sistemdeki gorevi: Baska bir uygulamanin modulu uzaktan cagirmasini saglar.
[DependsOn(
    typeof(GtfsManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class GtfsManagementHttpApiClientModule : AbpModule
{
    // Modulun servis ve secenek kayitlarini yapar.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(GtfsManagementApplicationContractsModule).Assembly,
            GtfsManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<GtfsManagementHttpApiClientModule>();
        });

    }
}
