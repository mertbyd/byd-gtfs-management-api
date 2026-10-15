using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.LocationGroups;
using Byd.GtfsManagement.Interfaces.LocationGroups;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: LocationGroup repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS konum grubu sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS konum grubu EF sorgularini uygular.</summary>
[ExposeServices(typeof(ILocationGroupRepository))]
public class EfCoreLocationGroupRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<LocationGroup>(dbContextProvider), ILocationGroupRepository
{
}
