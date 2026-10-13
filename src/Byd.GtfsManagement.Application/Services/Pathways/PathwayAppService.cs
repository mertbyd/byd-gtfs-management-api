using Byd.GtfsManagement.Dtos.Pathways;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Interfaces.Pathways;
using Byd.GtfsManagement.Managers.Pathways;
using Byd.GtfsManagement.Mappers.Pathways;
using Byd.GtfsManagement.Models.Pathways;

namespace Byd.GtfsManagement.Services.Pathways;

// islevi: Pathway CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS yuruyus yolu CRUD servisini ortak tabana baglar.</summary>
public class PathwayAppService
    : GtfsCrudAppServiceBase<
        Pathway, CreatePathwayDto, UpdatePathwayDto, PathwayDto, PathwayFilter,
        PathwayCreateModel, PathwayUpdateModel, PathwayManager, IPathwayRepository, PathwayMapper>,
        IPathwayAppService
{
}
