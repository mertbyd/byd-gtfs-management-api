using Byd.GtfsManagement.Dtos.StopAreas;
using Byd.GtfsManagement.Entities.StopAreas;
using Byd.GtfsManagement.Interfaces.StopAreas;
using Byd.GtfsManagement.Managers.StopAreas;
using Byd.GtfsManagement.Mappers.StopAreas;
using Byd.GtfsManagement.Models.StopAreas;

namespace Byd.GtfsManagement.Services.StopAreas;

// islevi: StopArea CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS durak bolge iliskisi CRUD servisini ortak tabana baglar.</summary>
public class StopAreaAppService
    : GtfsCrudAppServiceBase<
        StopArea, CreateStopAreaDto, UpdateStopAreaDto, StopAreaDto, StopAreaFilter,
        StopAreaCreateModel, StopAreaUpdateModel, StopAreaManager, IStopAreaRepository, StopAreaMapper>,
        IStopAreaAppService
{
}
