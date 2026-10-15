using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Networks;
using Byd.GtfsManagement.Interfaces.Networks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Network repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS ulasim agi sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS ulasim agi EF sorgularini uygular.</summary>
[ExposeServices(typeof(INetworkRepository))]
public class EfCoreNetworkRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Network>(dbContextProvider), INetworkRepository
{
}
