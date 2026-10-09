using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Managers.StopTimes;
using Byd.GtfsManagement.Mappers.StopTimes;
using Byd.GtfsManagement.Models.StopTimes;

namespace Byd.GtfsManagement.Services.StopTimes;

// islevi: StopTime CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
/// <summary>StopTime CRUD servisini ortak tabana baglar.</summary>
public class StopTimeAppService
    : GtfsCrudAppServiceBase<
        StopTime, CreateStopTimeDto, UpdateStopTimeDto, StopTimeDto, StopTimeFilter,
        StopTimeCreateModel, StopTimeUpdateModel, StopTimeManager, IStopTimeRepository, StopTimeMapper>, IStopTimeAppService
{
}
