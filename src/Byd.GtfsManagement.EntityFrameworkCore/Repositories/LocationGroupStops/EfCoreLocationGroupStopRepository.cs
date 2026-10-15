using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.LocationGroupStops;
using Byd.GtfsManagement.Interfaces.LocationGroupStops;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: LocationGroupStop repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS konum grubu durak iliskisi sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS konum grubu durak iliskisi EF sorgularini uygular.</summary>
[ExposeServices(typeof(ILocationGroupStopRepository))]
public class EfCoreLocationGroupStopRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<LocationGroupStop>(dbContextProvider), ILocationGroupStopRepository
{
}
