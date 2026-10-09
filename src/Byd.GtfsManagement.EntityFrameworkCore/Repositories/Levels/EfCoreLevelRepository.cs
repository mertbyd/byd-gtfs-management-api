using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Interfaces.Levels;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Level repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Kat sorgularini GtfsManagementDbContext uzerinden yurutur.
/// <summary>GTFS kat EF sorgularini uygular.</summary>
[ExposeServices(typeof(ILevelRepository))]
public class EfCoreLevelRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Level, Guid>(dbContextProvider), ILevelRepository
{
}
