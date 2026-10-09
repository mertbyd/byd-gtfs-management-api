using Byd.GtfsManagement.Dtos.FareAttributes;

namespace Byd.GtfsManagement.Services.FareAttributes;

// islevi: FareAttribute CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ucret ozelligi CRUD API sozlesmesini bildirir.</summary>
public interface IFareAttributeAppService
    : IGtfsCrudAppService<CreateFareAttributeDto, UpdateFareAttributeDto, FareAttributeDto, FareAttributeFilter>
{
}
