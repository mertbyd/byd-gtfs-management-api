using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.BookingRules;
using Byd.GtfsManagement.Interfaces.BookingRules;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.BookingRules;

namespace Byd.GtfsManagement.Managers.BookingRules;

// islevi: booking_rules.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS rezervasyon kurali is kurallarini yonetir.</summary>
public class BookingRuleManager(
    IBookingRuleRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<BookingRule, BookingRuleCreateModel, BookingRuleUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni rezervasyon kurali kaydini kurar.
    public override async Task<BookingRule> CreateAsync(
        BookingRuleCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new BookingRule(GuidGenerator.Create(), feed.TenantId, feed.Id, NormalizeRequiredText(model.GtfsBookingRuleId), model.BookingType);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        BookingRule entity, BookingRuleUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsBookingRuleId = NormalizeRequiredText(model.GtfsBookingRuleId);
        entity.BookingType = model.BookingType;
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(BookingRule entity, BookingRuleInputModel model)
    {
        entity.PriorNoticeDurationMin = model.PriorNoticeDurationMin;
        entity.PriorNoticeDurationMax = model.PriorNoticeDurationMax;
        entity.PriorNoticeLastDay = model.PriorNoticeLastDay;
        entity.PriorNoticeLastTime = NormalizeOptionalText(model.PriorNoticeLastTime);
        entity.PriorNoticeStartDay = model.PriorNoticeStartDay;
        entity.PriorNoticeStartTime = NormalizeOptionalText(model.PriorNoticeStartTime);
        entity.PriorNoticeServiceId = NormalizeOptionalText(model.PriorNoticeServiceId);
        entity.Message = NormalizeOptionalText(model.Message);
        entity.PickupMessage = NormalizeOptionalText(model.PickupMessage);
        entity.DropOffMessage = NormalizeOptionalText(model.DropOffMessage);
        entity.PhoneNumber = NormalizeOptionalText(model.PhoneNumber);
        entity.InfoUrl = NormalizeOptionalText(model.InfoUrl);
        entity.BookingUrl = NormalizeOptionalText(model.BookingUrl);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(BookingRule entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsBookingRuleId = entity.GtfsBookingRuleId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsBookingRuleId == gtfsBookingRuleId,
            entity.Id, cancellationToken);
    }
}
