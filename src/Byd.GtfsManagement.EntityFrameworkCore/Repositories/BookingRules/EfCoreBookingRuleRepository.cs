using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.BookingRules;
using Byd.GtfsManagement.Interfaces.BookingRules;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: BookingRule repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS rezervasyon kurali sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS rezervasyon kurali EF sorgularini uygular.</summary>
[ExposeServices(typeof(IBookingRuleRepository))]
public class EfCoreBookingRuleRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<BookingRule>(dbContextProvider), IBookingRuleRepository
{
}
