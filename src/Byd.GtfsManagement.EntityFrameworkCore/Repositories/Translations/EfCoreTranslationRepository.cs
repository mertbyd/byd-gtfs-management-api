using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Translations;
using Byd.GtfsManagement.Interfaces.Translations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Translation repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: GTFS ceviri sorgularini EF Core tabaniyla calistirir.
/// <summary>GTFS ceviri EF sorgularini uygular.</summary>
[ExposeServices(typeof(ITranslationRepository))]
public class EfCoreTranslationRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Translation, Guid>(dbContextProvider), ITranslationRepository
{
}
