using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Querying;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Interfaces.CalendarDates;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: CalendarDate repository sozlesmesini core EF tabanina baglar ve servis ile istisna turunu yukler.
// sistemdeki gorevi: Cevap DTO'sunun servis kimligi ve istisna adini tasiyabilmesi icin Include'u tek yerde tutar.
[ExposeServices(typeof(ICalendarDateRepository))]
public class EfCoreCalendarDateRepository(IDbContextProvider<GtfsManagementDbContext> dbContextProvider)
    : EfExtendedRepository<GtfsManagementDbContext, CalendarDate, Guid>(dbContextProvider), ICalendarDateRepository
{
    // Tekil okumalarda servis kimligi ve istisna adinin dolu gelmesini saglar.
    public override async Task<IQueryable<CalendarDate>> WithDetailsAsync()
        => IncludeNames(await GetQueryableAsync());

    // Liste sorgusunda da ayni adlari yukler; filtre ve sayfalama tabanda kalir.
    protected override async Task<IQueryable<CalendarDate>> CreateFilteredQueryAsync<TFiltering>(
        IDataTablePagingProperty<TFiltering> pagingProperty)
        => IncludeNames(await base.CreateFilteredQueryAsync(pagingProperty));

    // Cevap sozlesmesinin ad alanlarini besleyen navigasyonlari ekler.
    private static IQueryable<CalendarDate> IncludeNames(IQueryable<CalendarDate> query)
        => query
            .Include(calendarDate => calendarDate.Service)
            .Include(calendarDate => calendarDate.ExceptionType);
}
