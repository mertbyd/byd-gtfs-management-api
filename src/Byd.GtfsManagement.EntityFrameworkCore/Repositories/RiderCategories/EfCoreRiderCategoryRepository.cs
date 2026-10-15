using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.RiderCategories;
using Byd.GtfsManagement.Interfaces.RiderCategories;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: RiderCategory repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS yolcu kategorisi sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS yolcu kategorisi EF sorgularini uygular.</summary>
[ExposeServices(typeof(IRiderCategoryRepository))]
public class EfCoreRiderCategoryRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<RiderCategory>(dbContextProvider), IRiderCategoryRepository
{
}
