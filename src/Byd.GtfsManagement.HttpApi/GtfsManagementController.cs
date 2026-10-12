using Byd.GtfsManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Byd.GtfsManagement;

public abstract class GtfsManagementController : AbpControllerBase
{
    // ABP servislerini kullanim aninda cozer.
    protected T LazyGetRequiredService<T>() where T : notnull =>
        LazyServiceProvider.LazyGetRequiredService<T>();

    protected GtfsManagementController()
    {
        LocalizationResource = typeof(GtfsManagementResource);
    }
}
