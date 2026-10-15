using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Interfaces.Shapes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Shape repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Sekil sorgularini GtfsManagementDbContext uzerinden yurutur.
/// <summary>GTFS sekil EF sorgularini uygular.</summary>
[ExposeServices(typeof(IShapeRepository))]
public class EfCoreShapeRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Shape>(dbContextProvider), IShapeRepository
{
    // Feed'deki shape_id'nin noktalarini sira numarasina gore getirir.
    public virtual async Task<List<Shape>> GetPointsAsync(
        Guid feedId, string gtfsShapeId, CancellationToken cancellationToken = default)
        => await (await GetQueryableAsync())
            .Where(shape => shape.FeedId == feedId && shape.GtfsShapeId == gtfsShapeId)
            .OrderBy(shape => shape.Sequence)
            .ToListAsync(cancellationToken);
}
