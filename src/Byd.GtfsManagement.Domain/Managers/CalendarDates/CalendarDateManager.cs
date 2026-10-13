using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Entities.Lookup.ExceptionTypes;
using Byd.GtfsManagement.Extensions.Lookups;
using Byd.GtfsManagement.Interfaces.CalendarDates;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Services;
using Byd.GtfsManagement.Models.CalendarDates;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Managers.CalendarDates;

// islevi: calendar_dates.txt kaydinin istisna turunu ve gun basina teklik kuralini yonetir.
// sistemdeki gorevi: Takvim istisnasi kurallarinin tek sahibidir; kapsam ve silme guardlarini tabandan devralir.
public class CalendarDateManager(
    ICalendarDateRepository repository,
    FeedManager feedManager,
    ServiceManager serviceManager,
    IRepository<ExceptionType, int> exceptionTypeRepository)
    : GtfsFeedScopedManager<CalendarDate, CalendarDateCreateModel, CalendarDateUpdateModel>(
        repository, feedManager)
{
    // Hedef servisi kapsamda dogrulayip yeni istisnayi kurar.
    public override async Task<CalendarDate> CreateAsync(
        CalendarDateCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var service = await serviceManager.EnsureExistsInScopeAsync(model.ServiceId, cancellationToken);
        var entity = new CalendarDate(
            GuidGenerator.Create(), service.TenantId, service.FeedId)
        {
            ServiceId = service.Id
        };
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Yuklenmis istisnanin turunu ve gununu ayni kurallardan gecirerek gunceller.
    public override Task UpdateAsync(
        CalendarDate entity, CalendarDateUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Istisna turunu dogrular, gunu yazar ve servis basina gun tekilligini denetler.
    private async Task ApplyAsync(
        CalendarDate entity, CalendarDateInputModel model, CancellationToken cancellationToken = default)
    {
        entity.ExceptionTypeId = await exceptionTypeRepository.EnsureExistsAsync(
            model.ExceptionTypeId, cancellationToken);
        entity.Date = model.Date;
        await CheckServiceDateUniqueAsync(entity, cancellationToken);
    }

    // GTFS'te bir servis icin ayni tarihte yalnizca bir istisna bulunur.
    private Task CheckServiceDateUniqueAsync(CalendarDate entity, CancellationToken cancellationToken = default)
    {
        var serviceId = entity.ServiceId;
        var date = entity.Date;
        return CheckAlreadyExistsAsync(
            calendarDate => calendarDate.ServiceId == serviceId && calendarDate.Date == date,
            entity.Id,
            cancellationToken);
    }
}
