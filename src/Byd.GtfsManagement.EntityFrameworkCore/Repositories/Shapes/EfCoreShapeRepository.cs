using System;
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
    : EfExtendedRepository<GtfsManagementDbContext, Shape, Guid>(dbContextProvider), IShapeRepository
{
}
