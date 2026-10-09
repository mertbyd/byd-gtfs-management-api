using Byd.GtfsManagement.Dtos.Agencies;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Interfaces.Agencies;
using Byd.GtfsManagement.Managers.Agencies;
using Byd.GtfsManagement.Mappers.Agencies;
using Byd.GtfsManagement.Models.Agencies;

namespace Byd.GtfsManagement.Services.Agencies;

// islevi: Agency kayitlarinin CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class AgencyAppService
    : GtfsCrudAppServiceBase<
        Agency, CreateAgencyDto, UpdateAgencyDto, AgencyDto, AgencyFilter,
        AgencyCreateModel, AgencyUpdateModel, AgencyManager, IAgencyRepository, AgencyMapper>, IAgencyAppService
{
}
