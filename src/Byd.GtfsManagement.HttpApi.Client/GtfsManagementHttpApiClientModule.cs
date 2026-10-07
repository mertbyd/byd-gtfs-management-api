using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Byd.GtfsManagement;

[DependsOn(
    typeof(GtfsManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class GtfsManagementHttpApiClientModule : AbpModule
{
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
