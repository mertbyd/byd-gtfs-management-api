using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Interfaces.Stops;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Stop repository sozlesmesini core EF tabanina baglar ve lookup ile ust istasyon navigasyonlarini yukler.
// sistemdeki gorevi: Cevap DTO'sunun lookup ve istasyon adlarini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(IStopRepository))]
public class EfCoreStopRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, Stop, Guid>(dbContextProvider), IStopRepository
{
    // Tekil okumalarda konum turu, erisim durumu ve ust istasyon adlarinin dolu gelmesini saglar.
    public override async Task<IQueryable<Stop>> WithDetailsAsync()
        => IncludeNames(await GetQueryableAsync());

    // Liste sorgusunda da ayni adlari yukler; filtre, siralama ve sayfalama tabanda kalir.
    protected override async Task<IQueryable<Stop>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeNames(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin ad alanlarini besleyen navigasyonlari ekler.
    private static IQueryable<Stop> IncludeNames(IQueryable<Stop> query)
        => query
            .Include(stop => stop.LocationType)
            .Include(stop => stop.WheelchairBoarding)
            .Include(stop => stop.ParentStation);
}
