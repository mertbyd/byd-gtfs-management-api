using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Settings;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Interfaces.Stops;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace Byd.GtfsManagement.Handlers;

/// <summary>
/// Aktif olan Feed'in durak ve guzergahlarini organizasyonun arama indeksine yazar.
/// </summary>
// islevi: Kaydedilen Feed aktifse organizasyonun durak ve guzergah indekslerini o Feed'in kayitlariyla yeniden doldurur;
// pasif Feed'de ya da Elasticsearch kapaliyken hicbir sey yapmaz.
// sistemdeki gorevi: Yayin secimi degistiginde arama uclarinin yeni surumu gormesini saglar. Aktif Feed salt okunur
// oldugu icin yayindaki veri yalnizca bu anda degisir.
public class FeedUpdatedEventHandler(
    IStopRepository stopRepository,
    IRouteRepository routeRepository,
    ISettingProvider settingProvider,
    ICurrentTenant currentTenant,
    ICurrentOrganization currentOrganization)
    : ILocalEventHandler<EntityUpdatedEventData<Feed>>, ITransientDependency
{
    // Feed'in kapsamina girer; durak ve guzergah indekslerini aktif Feed'in kayitlariyla degistirir.
    [UnitOfWork]
    public virtual async Task HandleEventAsync(EntityUpdatedEventData<Feed> eventData)
    {
        var feed = eventData.Entity;
        if (!feed.IsActive || !await settingProvider.IsTrueAsync(CoreExtensionsSettings.Elasticsearch.IsEnabled))
            return;
        using var tenantScope = currentTenant.Change(feed.TenantId);
        using var organizationScope = currentOrganization.Change(feed.OrganizationId);
        await stopRepository.ReplaceSearchIndexAsync(feed.Id);
        await routeRepository.ReplaceSearchIndexAsync(feed.Id);
    }
}
