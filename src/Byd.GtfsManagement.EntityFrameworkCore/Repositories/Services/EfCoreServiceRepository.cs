using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Interfaces.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Service repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Core'un tenant ve aktiflik filtrelerini kullanir.
[ExposeServices(typeof(IServiceRepository))]
public class EfCoreServiceRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Service, Guid>(dbContextProvider), IServiceRepository
{
}
