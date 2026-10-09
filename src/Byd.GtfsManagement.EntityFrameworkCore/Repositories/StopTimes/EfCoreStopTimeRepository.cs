using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: StopTime repository sozlesmesini core EF tabanina baglar.
// sistemdeki gorevi: Cevap DTO'sunun sefer, durak ve lookup adlarini yukler.
/// <summary>StopTime EF sorgularini uygular.</summary>
[ExposeServices(typeof(IStopTimeRepository))]
public class EfCoreStopTimeRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, StopTime, Guid>(dbContextProvider), IStopTimeRepository
{
    // Tek kayit okumalarinda sefer, durak ve lookup adlarini yukler.
    public override async Task<IQueryable<StopTime>> WithDetailsAsync()
        => IncludeNames(await GetQueryableAsync());

    // Filtreli listede ayni iliskili adlari sorguya ekler.
    protected override async Task<IQueryable<StopTime>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeNames(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin kullandigi navigation alanlarini yukler.
    private static IQueryable<StopTime> IncludeNames(IQueryable<StopTime> query)
        => query
            .Include(value => value.Trip)
            .Include(value => value.Stop)
            .Include(value => value.LocationGroup)
            .Include(value => value.PickupType)
            .Include(value => value.DropOffType)
            .Include(value => value.ContinuousPickup)
            .Include(value => value.ContinuousDropOff);
}
