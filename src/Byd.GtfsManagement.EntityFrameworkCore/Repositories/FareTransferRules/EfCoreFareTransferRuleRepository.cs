using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FareTransferRules;
using Byd.GtfsManagement.Interfaces.FareTransferRules;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FareTransferRule repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS ucret aktarma kurali sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS ucret aktarma kurali EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFareTransferRuleRepository))]
public class EfCoreFareTransferRuleRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, FareTransferRule, Guid>(dbContextProvider), IFareTransferRuleRepository
{
}
