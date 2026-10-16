using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FareLegRules;
using Byd.GtfsManagement.Interfaces.FareLegRules;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.FareLegRules;

namespace Byd.GtfsManagement.Managers.FareLegRules;

// islevi: fare_leg_rules.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ucret etap kurali is kurallarini yonetir.</summary>
public class FareLegRuleManager(
    IFareLegRuleRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<FareLegRule, FareLegRuleCreateModel, FareLegRuleUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni ucret etap kurali kaydini kurar.
    public override async Task<FareLegRule> CreateAsync(
        FareLegRuleCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new FareLegRule(GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.GtfsFareProductId));
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        FareLegRule entity, FareLegRuleUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsFareProductId = NormalizeRequiredText(model.GtfsFareProductId);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(FareLegRule entity, FareLegRuleInputModel model)
    {
        entity.LegGroupId = NormalizeOptionalText(model.LegGroupId);
        entity.NetworkId = NormalizeOptionalText(model.NetworkId);
        entity.FromAreaId = NormalizeOptionalText(model.FromAreaId);
        entity.ToAreaId = NormalizeOptionalText(model.ToAreaId);
        entity.FromTimeframeGroupId = NormalizeOptionalText(model.FromTimeframeGroupId);
        entity.ToTimeframeGroupId = NormalizeOptionalText(model.ToTimeframeGroupId);
        entity.RulePriority = model.RulePriority;
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(FareLegRule entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var legGroupId = entity.LegGroupId;
        var networkId = entity.NetworkId;
        var fromAreaId = entity.FromAreaId;
        var toAreaId = entity.ToAreaId;
        var fromTimeframeGroupId = entity.FromTimeframeGroupId;
        var toTimeframeGroupId = entity.ToTimeframeGroupId;
        var gtfsFareProductId = entity.GtfsFareProductId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.LegGroupId == legGroupId && value.NetworkId == networkId && value.FromAreaId == fromAreaId && value.ToAreaId == toAreaId && value.FromTimeframeGroupId == fromTimeframeGroupId && value.ToTimeframeGroupId == toTimeframeGroupId && value.GtfsFareProductId == gtfsFareProductId,
            entity.Id, cancellationToken);
    }
}
