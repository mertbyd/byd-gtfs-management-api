using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Areas;
using Byd.GtfsManagement.Interfaces.Areas;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Area repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS bolge sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS bolge EF sorgularini uygular.</summary>
[ExposeServices(typeof(IAreaRepository))]
public class EfCoreAreaRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Area>(dbContextProvider), IAreaRepository
{
}
