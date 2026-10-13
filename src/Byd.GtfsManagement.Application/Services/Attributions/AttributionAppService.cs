using Byd.GtfsManagement.Dtos.Attributions;
using Byd.GtfsManagement.Entities.Attributions;
using Byd.GtfsManagement.Interfaces.Attributions;
using Byd.GtfsManagement.Managers.Attributions;
using Byd.GtfsManagement.Mappers.Attributions;
using Byd.GtfsManagement.Models.Attributions;

namespace Byd.GtfsManagement.Services.Attributions;

// islevi: Attribution CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS atif CRUD servisini ortak tabana baglar.</summary>
public class AttributionAppService
    : GtfsCrudAppServiceBase<
        Attribution, CreateAttributionDto, UpdateAttributionDto, AttributionDto, AttributionFilter,
        AttributionCreateModel, AttributionUpdateModel, AttributionManager, IAttributionRepository, AttributionMapper>,
        IAttributionAppService
{
}
