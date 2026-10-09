using Byd.GtfsManagement.Dtos.Attributions;

namespace Byd.GtfsManagement.Services.Attributions;

// islevi: Attribution CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS atif CRUD API sozlesmesini bildirir.</summary>
public interface IAttributionAppService
    : IGtfsCrudAppService<CreateAttributionDto, UpdateAttributionDto, AttributionDto, AttributionFilter>
{
}
