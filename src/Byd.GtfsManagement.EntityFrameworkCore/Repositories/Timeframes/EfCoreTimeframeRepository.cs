using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Timeframes;
using Byd.GtfsManagement.Interfaces.Timeframes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Timeframe repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS zaman araligi sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS zaman araligi EF sorgularini uygular.</summary>
[ExposeServices(typeof(ITimeframeRepository))]
public class EfCoreTimeframeRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Timeframe, Guid>(dbContextProvider), ITimeframeRepository
{
}
