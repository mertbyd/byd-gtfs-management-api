using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FareLegJoinRules;
using Byd.GtfsManagement.Interfaces.FareLegJoinRules;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareLegJoinRule repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS ucret etap birlestirme kurali sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS ucret etap birlestirme kurali EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFareLegJoinRuleRepository))]
public class EfCoreFareLegJoinRuleRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<FareLegJoinRule>(dbContextProvider), IFareLegJoinRuleRepository
{
}
