using Byd.GtfsManagement.Dtos.FareProducts;

namespace Byd.GtfsManagement.Services.FareProducts;

// islevi: FareProduct CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ucret urunu CRUD API sozlesmesini bildirir.</summary>
public interface IFareProductAppService
    : IGtfsCrudAppService<CreateFareProductDto, UpdateFareProductDto, FareProductDto, FareProductFilter>
{
}
