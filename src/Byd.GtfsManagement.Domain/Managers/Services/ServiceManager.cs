using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Interfaces.Services;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Services;

namespace Byd.GtfsManagement.Managers.Services;

// islevi: GTFS service_id kaydinin kimlik tekilligini yonetir.
// sistemdeki gorevi: Takvim ve sefer kayitlarinin bagli oldugu servis kimliginin tek sahibidir.
public class ServiceManager(
    IServiceRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<Service, ServiceCreateModel, ServiceUpdateModel>(repository, feedManager)
{
    // Hedef Feed'i kapsamda dogrulayip yeni servisi kurar.
    public override async Task<Service> CreateAsync(
        ServiceCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Service(
            GuidGenerator.Create(), feed.Id);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Yuklenmis servisin kimligini ayni kurallardan gecirerek gunceller.
    public override Task UpdateAsync(
        Service entity, ServiceUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Kimligi normalize eder ve Feed icinde tekil olmasini dogrular.
    private Task ApplyAsync(Service entity, ServiceInputModel model, CancellationToken cancellationToken = default)
    {
        entity.GtfsServiceId = NormalizeRequiredText(model.GtfsServiceId);
        var feedId = entity.FeedId;
        var gtfsServiceId = entity.GtfsServiceId;
        return CheckAlreadyExistsAsync(
            service => service.FeedId == feedId && service.GtfsServiceId == gtfsServiceId,
            entity.Id,
            cancellationToken);
    }
}
