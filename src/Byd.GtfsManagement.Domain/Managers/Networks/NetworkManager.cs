using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Networks;
using Byd.GtfsManagement.Interfaces.Networks;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Networks;

namespace Byd.GtfsManagement.Managers.Networks;

// islevi: networks.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ulasim agi is kurallarini yonetir.</summary>
public class NetworkManager(
    INetworkRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<Network, NetworkCreateModel, NetworkUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni ulasim agi kaydini kurar.
    public override async Task<Network> CreateAsync(
        NetworkCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new Network(GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.GtfsNetworkId));
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        Network entity, NetworkUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsNetworkId = NormalizeRequiredText(model.GtfsNetworkId);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(Network entity, NetworkInputModel model)
    {
        entity.NetworkName = NormalizeOptionalText(model.NetworkName);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(Network entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsNetworkId = entity.GtfsNetworkId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsNetworkId == gtfsNetworkId,
            entity.Id, cancellationToken);
    }
}
