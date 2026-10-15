using Byd.GtfsManagement.Dtos.FareProducts;
using Byd.GtfsManagement.Entities.FareProducts;
using Byd.GtfsManagement.Interfaces.FareProducts;
using Byd.GtfsManagement.Managers.FareProducts;
using Byd.GtfsManagement.Mappers.FareProducts;
using Byd.GtfsManagement.Models.FareProducts;

namespace Byd.GtfsManagement.Services.FareProducts;

// islevi: FareProduct CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ucret urunu CRUD servisini ortak tabana baglar.</summary>
public class FareProductAppService
    : GtfsImportExportAppServiceBase<
        FareProduct, CreateFareProductDto, UpdateFareProductDto, FareProductDto, FareProductFilter,
        FareProductCreateModel, FareProductUpdateModel, FareProductManager, IFareProductRepository, FareProductMapper, FareProductImportDto>,
        IFareProductAppService
{
}
