using Byd.GtfsManagement.Localization;
using Volo.Abp.Application.Services;

namespace Byd.GtfsManagement;

public abstract class GtfsManagementAppService : ApplicationService
{
    // ABP servislerini kullanim aninda cozer.
    protected T LazyGetRequiredService<T>() where T : notnull =>
        LazyServiceProvider.LazyGetRequiredService<T>();

    protected GtfsManagementAppService()
    {
        LocalizationResource = typeof(GtfsManagementResource);
        ObjectMapperContext = typeof(GtfsManagementApplicationModule);
    }
}
