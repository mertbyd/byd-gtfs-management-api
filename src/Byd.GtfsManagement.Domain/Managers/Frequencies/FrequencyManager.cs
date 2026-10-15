using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Frequencies;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Gtfs;
using Byd.GtfsManagement.Interfaces.Frequencies;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Trips;
using Byd.GtfsManagement.Models.Frequencies;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.Frequencies;

// islevi: frequencies.txt sefer, saat, headway ve aralik kurallarini korur.
// sistemdeki gorevi: Her siklik kaydini tek bir Trip ve Feed kapsaminda yonetir.
/// <summary>GTFS sefer sikligi is kurallarini yonetir.</summary>
public class FrequencyManager(
    IFrequencyRepository repository,
    FeedManager feedManager,
    TripManager tripManager)
    : GtfsFeedScopedManager<Frequency, FrequencyCreateModel, FrequencyUpdateModel>(
        repository, feedManager)
{
    // Kapsamdaki seferden yeni siklik kaydini kurar.
    public override async Task<Frequency> CreateAsync(
        FrequencyCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var trip = await tripManager.EnsureExistsInScopeAsync(model.TripId, cancellationToken);
        var entity = new Frequency(
            GuidGenerator.Create(), trip.FeedId)
        {
            TripId = trip.Id
        };
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Sefer bagini koruyarak siklik alanlarini gunceller.
    public override Task UpdateAsync(
        Frequency entity, FrequencyUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Saat sirasini ve ayni seferin diger araliklariyla cakismamayi dogrular.
    private async Task ApplyAsync(
        Frequency entity, FrequencyInputModel model, CancellationToken cancellationToken = default)
    {
        entity.StartTime = NormalizeRequiredText(model.StartTime);
        entity.EndTime = NormalizeRequiredText(model.EndTime);
        if (entity.StartTime.CompareGtfsTimes(entity.EndTime) >= 0)
            throw new BusinessException(FrequencyExceptionCodes.TimeOrderInvalid);
        entity.HeadwaySecs = model.HeadwaySecs;
        entity.ExactTimes = model.ExactTimes;
        await EnsureNoOverlapAsync(entity, cancellationToken);
    }

    // Bir seferin yari acik siklik araliklarinin cakismamasini saglar.
    private async Task EnsureNoOverlapAsync(Frequency entity, CancellationToken cancellationToken = default)
    {
        var tripId = entity.TripId;
        var id = entity.Id;
        var others = await repository.GetListAsync(
            other => other.TripId == tripId && other.Id != id, cancellationToken: cancellationToken);
        foreach (var other in others)
        {
            if (entity.StartTime.CompareGtfsTimes(other.EndTime) < 0
                && other.StartTime.CompareGtfsTimes(entity.EndTime) < 0)
                throw new BusinessException(FrequencyExceptionCodes.IntervalOverlaps);
        }
    }
}
