using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.RouteNetworks;
using Byd.GtfsManagement.Interfaces.RouteNetworks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: RouteNetwork repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS hat ag iliskisi sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS hat ag iliskisi EF sorgularini uygular.</summary>
[ExposeServices(typeof(IRouteNetworkRepository))]
public class EfCoreRouteNetworkRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, RouteNetwork, Guid>(dbContextProvider), IRouteNetworkRepository
{
}
