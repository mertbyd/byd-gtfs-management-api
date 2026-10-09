using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FareLegRules;
using Byd.GtfsManagement.Interfaces.FareLegRules;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareLegRule repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS ucret etap kurali sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS ucret etap kurali EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFareLegRuleRepository))]
public class EfCoreFareLegRuleRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, FareLegRule, Guid>(dbContextProvider), IFareLegRuleRepository
{
}
