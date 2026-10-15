using System.Threading.Tasks;
using Byd.GtfsManagement.Etos.Feeds;
using Byd.GtfsManagement.Executors;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Byd.GtfsManagement.Handlers;

/// <summary>Entegrasyon olayini Feed'in kapsaminda ortak ice aktarma adimina yonlendirir.</summary>
public class FeedIntegrationRequestedEventHandler(
    FeedIntegrationExecutor executor)
    : IDistributedEventHandler<FeedIntegrationRequestedEto>, ITransientDependency
{
    // Olay kapsamini ortak entegrasyon akisina iletir.
    public virtual Task HandleEventAsync(FeedIntegrationRequestedEto eventData)
        => executor.IntegrateAsync(eventData.FeedId, eventData.TenantId, eventData.OrganizationId);
}
