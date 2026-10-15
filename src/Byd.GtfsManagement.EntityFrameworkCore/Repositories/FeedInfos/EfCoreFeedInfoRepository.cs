using System;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.FeedInfos;
using Byd.GtfsManagement.Interfaces.FeedInfos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: FeedInfo repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Yayimci sorgularini GtfsManagementDbContext uzerinden yurutur.
/// <summary>GTFS yayimci bilgisi EF sorgularini uygular.</summary>
[ExposeServices(typeof(IFeedInfoRepository))]
public class EfCoreFeedInfoRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<FeedInfo>(dbContextProvider), IFeedInfoRepository
{
}
