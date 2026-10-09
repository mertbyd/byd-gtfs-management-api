using Byd.GtfsManagement.Dtos.RiderCategories;
using Byd.GtfsManagement.Entities.RiderCategories;
using Byd.GtfsManagement.Interfaces.RiderCategories;
using Byd.GtfsManagement.Managers.RiderCategories;
using Byd.GtfsManagement.Mappers.RiderCategories;
using Byd.GtfsManagement.Models.RiderCategories;

namespace Byd.GtfsManagement.Services.RiderCategories;

// islevi: RiderCategory CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS yolcu kategorisi CRUD servisini ortak tabana baglar.</summary>
public class RiderCategoryAppService
    : GtfsCrudAppServiceBase<
        RiderCategory, CreateRiderCategoryDto, UpdateRiderCategoryDto, RiderCategoryDto, RiderCategoryFilter,
        RiderCategoryCreateModel, RiderCategoryUpdateModel, RiderCategoryManager, IRiderCategoryRepository, RiderCategoryMapper>,
        IRiderCategoryAppService
{
}
