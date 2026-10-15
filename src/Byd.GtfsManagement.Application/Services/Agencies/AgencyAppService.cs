using Byd.GtfsManagement.Dtos.Agencies;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Interfaces.Agencies;
using Byd.GtfsManagement.Managers.Agencies;
using Byd.GtfsManagement.Mappers.Agencies;
using Byd.GtfsManagement.Models.Agencies;

namespace Byd.GtfsManagement.Services.Agencies;

// islevi: Agency kayitlarinin CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class AgencyAppService
    : GtfsImportExportAppServiceBase<
        Agency, CreateAgencyDto, UpdateAgencyDto, AgencyDto, AgencyFilter,
        AgencyCreateModel, AgencyUpdateModel, AgencyManager, IAgencyRepository, AgencyMapper, AgencyImportDto>, IAgencyAppService
{
}
