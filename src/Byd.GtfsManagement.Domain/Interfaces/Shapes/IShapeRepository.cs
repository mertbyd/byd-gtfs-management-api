using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Shapes;

namespace Byd.GtfsManagement.Interfaces.Shapes;

// islevi: Shape icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
/// <summary>GTFS sekil veri erisim sozlesmesini bildirir.</summary>
public interface IShapeRepository : IExtendedRepository<Shape>
{
}
