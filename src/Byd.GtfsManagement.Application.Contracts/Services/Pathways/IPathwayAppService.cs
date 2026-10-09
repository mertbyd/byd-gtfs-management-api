using Byd.GtfsManagement.Dtos.Pathways;

namespace Byd.GtfsManagement.Services.Pathways;

// islevi: Pathway CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS yuruyus yolu CRUD API sozlesmesini bildirir.</summary>
public interface IPathwayAppService
    : IGtfsCrudAppService<CreatePathwayDto, UpdatePathwayDto, PathwayDto, PathwayFilter>
{
}
