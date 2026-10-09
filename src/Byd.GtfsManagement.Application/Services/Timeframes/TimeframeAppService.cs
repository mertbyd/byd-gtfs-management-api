using Byd.GtfsManagement.Dtos.Timeframes;
using Byd.GtfsManagement.Entities.Timeframes;
using Byd.GtfsManagement.Interfaces.Timeframes;
using Byd.GtfsManagement.Managers.Timeframes;
using Byd.GtfsManagement.Mappers.Timeframes;
using Byd.GtfsManagement.Models.Timeframes;

namespace Byd.GtfsManagement.Services.Timeframes;

// islevi: Timeframe CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS zaman araligi CRUD servisini ortak tabana baglar.</summary>
public class TimeframeAppService
    : GtfsCrudAppServiceBase<
        Timeframe, CreateTimeframeDto, UpdateTimeframeDto, TimeframeDto, TimeframeFilter,
        TimeframeCreateModel, TimeframeUpdateModel, TimeframeManager, ITimeframeRepository, TimeframeMapper>,
        ITimeframeAppService
{
}
