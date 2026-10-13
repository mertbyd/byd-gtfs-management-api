using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Repositories;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Entities.Calendars;
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
    // Verilen gunde calisan servisleri calendar haftalik kurali ve calendar_dates istisnalariyla bulur.
    public virtual async Task<List<Guid>> GetActiveIdsAsync(Guid feedId, DateOnly date, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var weekly = dbContext.Set<Calendar>()
            .Where(calendar => calendar.FeedId == feedId && calendar.StartDate <= date && calendar.EndDate >= date)
            .Where(RunsOn(date.DayOfWeek))
            .Select(calendar => calendar.ServiceId);
        var exceptions = dbContext.Set<CalendarDate>().Where(exception => exception.FeedId == feedId && exception.Date == date);
        var added = exceptions.Where(exception => exception.ExceptionType.Code == ExceptionTypeConsts.ServiceAddedCode)
            .Select(exception => exception.ServiceId);
        var removed = exceptions.Where(exception => exception.ExceptionType.Code == ExceptionTypeConsts.ServiceRemovedCode)
            .Select(exception => exception.ServiceId);
        return await weekly.Union(added).Except(removed).ToListAsync(cancellationToken);
    }

    // Haftanin gunune gore calendar bayrak kolonunu secen kosulu verir.
    private static Expression<Func<Calendar, bool>> RunsOn(DayOfWeek day) => day switch
    {
        DayOfWeek.Monday => calendar => calendar.Monday,
        DayOfWeek.Tuesday => calendar => calendar.Tuesday,
        DayOfWeek.Wednesday => calendar => calendar.Wednesday,
        DayOfWeek.Thursday => calendar => calendar.Thursday,
        DayOfWeek.Friday => calendar => calendar.Friday,
        DayOfWeek.Saturday => calendar => calendar.Saturday,
        _ => calendar => calendar.Sunday
    };
}
