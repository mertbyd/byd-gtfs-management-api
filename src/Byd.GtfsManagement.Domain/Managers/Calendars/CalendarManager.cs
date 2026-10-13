using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Calendars;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.Calendars;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Services;
using Byd.GtfsManagement.Models.Calendars;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.Calendars;

// islevi: calendar.txt kaydinin tarih araligini ve servis basina teklik kuralini yonetir.
// sistemdeki gorevi: Haftalik hizmet deseni kurallarinin tek sahibidir; kapsam ve silme guardlarini tabandan devralir.
public class CalendarManager(
    ICalendarRepository repository,
    FeedManager feedManager,
    ServiceManager serviceManager)
    : GtfsFeedScopedManager<Calendar, CalendarCreateModel, CalendarUpdateModel>(repository, feedManager)
{
    // Hedef servisi kapsamda dogrulayip yeni haftalik deseni kurar.
    public override async Task<Calendar> CreateAsync(
        CalendarCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var service = await serviceManager.EnsureExistsInScopeAsync(model.ServiceId, cancellationToken);
        var entity = new Calendar(
            GuidGenerator.Create(), service.TenantId, service.FeedId)
        {
            ServiceId = service.Id
        };
        await CheckServiceHasNoCalendarAsync(entity, cancellationToken);
        Apply(entity, model);
        return entity;
    }

    // Yuklenmis desenin gun ve tarih alanlarini ayni kurallardan gecirerek gunceller.
    public override Task UpdateAsync(
        Calendar entity, CalendarUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        Apply(entity, model);
        return Task.CompletedTask;
    }

    // Haftalik gunleri ve gecerlilik araligini yazar.
    private static void Apply(Calendar entity, CalendarInputModel model)
    {
        entity.Monday = model.Monday;
        entity.Tuesday = model.Tuesday;
        entity.Wednesday = model.Wednesday;
        entity.Thursday = model.Thursday;
        entity.Friday = model.Friday;
        entity.Saturday = model.Saturday;
        entity.Sunday = model.Sunday;
        entity.StartDate = model.StartDate;
        entity.EndDate = EnsureRangeOrdered(model.StartDate, model.EndDate);
    }

    // GTFS'te end_date start_date'ten once olamaz.
    private static DateOnly EnsureRangeOrdered(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
            throw new BusinessException(CalendarExceptionCodes.EndDateBeforeStartDate);
        return endDate;
    }

    // GTFS'te bir servisin en fazla bir haftalik deseni bulunur.
    private Task CheckServiceHasNoCalendarAsync(Calendar entity, CancellationToken cancellationToken = default)
    {
        var serviceId = entity.ServiceId;
        return CheckAlreadyExistsAsync(
            calendar => calendar.ServiceId == serviceId,
            entity.Id,
            cancellationToken);
    }
}
