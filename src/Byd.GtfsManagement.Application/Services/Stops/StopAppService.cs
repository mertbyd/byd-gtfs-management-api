using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Interfaces.Stops;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Mappers.Stops;
using Byd.GtfsManagement.Models.Stops;

namespace Byd.GtfsManagement.Services.Stops;

// islevi: Stop kayitlarinin CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class StopAppService
    : GtfsCrudAppServiceBase<
        Stop, CreateStopDto, UpdateStopDto, StopDto, StopFilter,
        StopCreateModel, StopUpdateModel, StopManager, IStopRepository, StopMapper>, IStopAppService
{
}
