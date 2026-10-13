using Byd.GtfsManagement.Dtos.Shapes;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Interfaces.Shapes;
using Byd.GtfsManagement.Managers.Shapes;
using Byd.GtfsManagement.Mappers.Shapes;
using Byd.GtfsManagement.Models.Shapes;

namespace Byd.GtfsManagement.Services.Shapes;

// islevi: Shape CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS sekil CRUD servisini ortak tabana baglar.</summary>
public class ShapeAppService
    : GtfsCrudAppServiceBase<
        Shape, CreateShapeDto, UpdateShapeDto, ShapeDto, ShapeFilter,
        ShapeCreateModel, ShapeUpdateModel, ShapeManager, IShapeRepository, ShapeMapper>,
        IShapeAppService
{
}
