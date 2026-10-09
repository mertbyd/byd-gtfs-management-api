using Byd.GtfsManagement.Dtos.Shapes;

namespace Byd.GtfsManagement.Services.Shapes;

// islevi: Shape CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS sekil CRUD API sozlesmesini bildirir.</summary>
public interface IShapeAppService
    : IGtfsCrudAppService<CreateShapeDto, UpdateShapeDto, ShapeDto, ShapeFilter>
{
}
