using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.StopAreas;
using Byd.GtfsManagement.Interfaces.StopAreas;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: StopArea repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS durak bolge iliskisi sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS durak bolge iliskisi EF sorgularini uygular.</summary>
[ExposeServices(typeof(IStopAreaRepository))]
public class EfCoreStopAreaRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, StopArea, Guid>(dbContextProvider), IStopAreaRepository
{
}
