using Byd.GtfsManagement.Dtos.RiderCategories;

namespace Byd.GtfsManagement.Services.RiderCategories;

// islevi: RiderCategory CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS yolcu kategorisi CRUD API sozlesmesini bildirir.</summary>
public interface IRiderCategoryAppService
    : IGtfsCrudAppService<CreateRiderCategoryDto, UpdateRiderCategoryDto, RiderCategoryDto, RiderCategoryFilter>
{
}
