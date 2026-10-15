using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Shapes;

namespace Byd.GtfsManagement.Interfaces.Shapes;

// islevi: Shape icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
/// <summary>GTFS sekil veri erisim sozlesmesini bildirir.</summary>
public interface IShapeRepository : IExtendedRepository<Shape>
{
    // Feed'deki shape_id'nin noktalarini sira numarasina gore getirir.
    Task<List<Shape>> GetPointsAsync(Guid feedId, string gtfsShapeId, CancellationToken cancellationToken = default);
}
