using Byd.GtfsManagement.Dtos.Areas;
using Byd.GtfsManagement.Entities.Areas;
using Byd.GtfsManagement.Interfaces.Areas;
using Byd.GtfsManagement.Managers.Areas;
using Byd.GtfsManagement.Mappers.Areas;
using Byd.GtfsManagement.Models.Areas;

namespace Byd.GtfsManagement.Services.Areas;

// islevi: Area CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS bolge CRUD servisini ortak tabana baglar.</summary>
public class AreaAppService
    : GtfsCrudAppServiceBase<
        Area, CreateAreaDto, UpdateAreaDto, AreaDto, AreaFilter,
        AreaCreateModel, AreaUpdateModel, AreaManager, IAreaRepository, AreaMapper>,
        IAreaAppService
{
}
