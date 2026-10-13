using Byd.GtfsManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Byd.GtfsManagement;

// islevi: GTFS controller'larinin ortak tabanidir; lokalizasyon kaynagini ve servislerin gec cozumunu saglar.
// sistemdeki gorevi: Her GTFS HTTP ucu bu tabandan turer.
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
