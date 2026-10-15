using Byd.GtfsManagement.Dtos.Services;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Interfaces.Services;
using Byd.GtfsManagement.Managers.Services;
using Byd.GtfsManagement.Mappers.Services;
using Byd.GtfsManagement.Models.Services;

namespace Byd.GtfsManagement.Services.Services;

// islevi: Service kayitlarinin CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class ServiceAppService
    : GtfsImportExportAppServiceBase<
        Service, CreateServiceDto, UpdateServiceDto, ServiceDto, ServiceFilter,
        ServiceCreateModel, ServiceUpdateModel, ServiceManager, IServiceRepository, ServiceMapper, ServiceImportDto>, IServiceAppService
{
}
