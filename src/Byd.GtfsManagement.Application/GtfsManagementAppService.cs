using Byd.GtfsManagement.Localization;
using Volo.Abp.Application.Services;

namespace Byd.GtfsManagement;

// islevi: GTFS AppService'lerinin ortak tabanidir; lokalizasyon kaynagini ve servislerin gec cozumunu saglar.
// sistemdeki gorevi: Her GTFS use case servisi bu tabandan turer.
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
