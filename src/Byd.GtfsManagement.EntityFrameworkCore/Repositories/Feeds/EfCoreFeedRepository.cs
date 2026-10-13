using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces.Feeds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Feed repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Core'un tenant ve aktiflik filtrelerini Feed sorgularinda kullanir.
[ExposeServices(typeof(IFeedRepository))]
public class EfCoreFeedRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Feed, Guid>(dbContextProvider), IFeedRepository
{
}
