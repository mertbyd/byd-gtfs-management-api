using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FareLegJoinRules;
using Byd.GtfsManagement.Interfaces.FareLegJoinRules;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.FareLegJoinRules;

namespace Byd.GtfsManagement.Managers.FareLegJoinRules;

// islevi: fare_leg_join_rules.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ucret etap birlestirme kurali is kurallarini yonetir.</summary>
public class FareLegJoinRuleManager(
    IFareLegJoinRuleRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<FareLegJoinRule, FareLegJoinRuleCreateModel, FareLegJoinRuleUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni ucret etap birlestirme kurali kaydini kurar.
    public override async Task<FareLegJoinRule> CreateAsync(
        FareLegJoinRuleCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new FareLegJoinRule(GuidGenerator.Create(), feed.Id);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        FareLegJoinRule entity, FareLegJoinRuleUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(FareLegJoinRule entity, FareLegJoinRuleInputModel model)
    {
        entity.FromNetworkId = NormalizeOptionalText(model.FromNetworkId);
        entity.ToNetworkId = NormalizeOptionalText(model.ToNetworkId);
        entity.FromStopId = NormalizeOptionalText(model.FromStopId);
        entity.ToStopId = NormalizeOptionalText(model.ToStopId);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(FareLegJoinRule entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var fromNetworkId = entity.FromNetworkId;
        var toNetworkId = entity.ToNetworkId;
        var fromStopId = entity.FromStopId;
        var toStopId = entity.ToStopId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.FromNetworkId == fromNetworkId && value.ToNetworkId == toNetworkId && value.FromStopId == fromStopId && value.ToStopId == toStopId,
            entity.Id, cancellationToken);
    }
}
