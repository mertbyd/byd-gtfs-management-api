using Byd.GtfsManagement.Dtos.FareAttributes;
using Byd.GtfsManagement.Entities.FareAttributes;
using Byd.GtfsManagement.Interfaces.FareAttributes;
using Byd.GtfsManagement.Managers.FareAttributes;
using Byd.GtfsManagement.Mappers.FareAttributes;
using Byd.GtfsManagement.Models.FareAttributes;

namespace Byd.GtfsManagement.Services.FareAttributes;

// islevi: FareAttribute CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ucret ozelligi CRUD servisini ortak tabana baglar.</summary>
public class FareAttributeAppService
    : GtfsImportExportAppServiceBase<
        FareAttribute, CreateFareAttributeDto, UpdateFareAttributeDto, FareAttributeDto, FareAttributeFilter,
        FareAttributeCreateModel, FareAttributeUpdateModel, FareAttributeManager, IFareAttributeRepository, FareAttributeMapper, FareAttributeImportDto>,
        IFareAttributeAppService
{
}
