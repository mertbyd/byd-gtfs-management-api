using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Etos.Feeds;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Managers.Feeds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Byd.GtfsManagement.Handlers;

/// <summary>
/// Istenen Feed'i organizasyonun yayin secimi yapar.
/// </summary>
// islevi: Organizasyonun onceki aktif Feed'ini pasife alip secilen Feed'i aktife alir.
// sistemdeki gorevi: Istegin transaction'inda yayin secimini degistirir; GTFS satirlari degismez.
public class FeedActivationRequestedEventHandler(
    FeedManager feedManager,
    ICurrentTenant currentTenant,
    ICurrentOrganization currentOrganization)
    : ILocalEventHandler<FeedActivationRequestedEto>, ITransientDependency
{
    // Olayin kapsaminda surumu dogrular; Feed'in yayin secimini atomik olarak kaydeder.
    [UnitOfWork]
    public virtual async Task HandleEventAsync(FeedActivationRequestedEto eventData)
    {
        using var tenantScope = currentTenant.Change(eventData.TenantId);
        using var organizationScope = currentOrganization.Change(eventData.OrganizationId);
        var feed = await feedManager.EnsureExistsIncludingInactiveInScopeAsync(eventData.FeedId);
        await feedManager.ActivateAsync(feed);
    }
}
