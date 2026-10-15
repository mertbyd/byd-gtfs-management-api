using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FareMedias;
using Byd.GtfsManagement.Interfaces.FareMedias;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareMedia repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS ucret medyasi sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS ucret medyasi EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFareMediaRepository))]
public class EfCoreFareMediaRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<FareMedia>(dbContextProvider), IFareMediaRepository
{
}
