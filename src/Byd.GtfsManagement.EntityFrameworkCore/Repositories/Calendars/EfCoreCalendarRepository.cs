using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.Calendars;
using Byd.GtfsManagement.Interfaces.Calendars;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: Calendar repository sozlesmesini core EF tabanina baglar ve servis navigasyonunu yukler.
// sistemdeki gorevi: Cevap DTO'sunun GTFS servis kimligini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(ICalendarRepository))]
public class EfCoreCalendarRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfGtfsRepository<Calendar>(dbContextProvider), ICalendarRepository
{
    // Tekil okumalarda servis kimliginin dolu gelmesini saglar.
    public override async Task<IQueryable<Calendar>> WithDetailsAsync()
        => (await GetQueryableAsync()).Include(calendar => calendar.Service);

    // Liste sorgusunda da servis kimligini yukler; filtre ve sayfalama tabanda kalir.
    protected override async Task<IQueryable<Calendar>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => (await base.CreateFilteredQueryAsync(pagingProperty)).Include(calendar => calendar.Service);
}
