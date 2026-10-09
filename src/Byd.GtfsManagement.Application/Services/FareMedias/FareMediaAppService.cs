using Byd.GtfsManagement.Dtos.FareMedias;
using Byd.GtfsManagement.Entities.FareMedias;
using Byd.GtfsManagement.Interfaces.FareMedias;
using Byd.GtfsManagement.Managers.FareMedias;
using Byd.GtfsManagement.Mappers.FareMedias;
using Byd.GtfsManagement.Models.FareMedias;

namespace Byd.GtfsManagement.Services.FareMedias;

// islevi: FareMedia CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ucret medyasi CRUD servisini ortak tabana baglar.</summary>
public class FareMediaAppService
    : GtfsCrudAppServiceBase<
        FareMedia, CreateFareMediaDto, UpdateFareMediaDto, FareMediaDto, FareMediaFilter,
        FareMediaCreateModel, FareMediaUpdateModel, FareMediaManager, IFareMediaRepository, FareMediaMapper>,
        IFareMediaAppService
{
}
