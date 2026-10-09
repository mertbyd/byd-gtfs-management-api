using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FareProducts;
using Byd.GtfsManagement.Interfaces.FareProducts;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareProduct repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS ucret urunu sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS ucret urunu EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFareProductRepository))]
public class EfCoreFareProductRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, FareProduct, Guid>(dbContextProvider), IFareProductRepository
{
}
