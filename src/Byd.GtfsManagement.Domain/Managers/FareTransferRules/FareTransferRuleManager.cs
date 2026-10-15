using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FareTransferRules;
using Byd.GtfsManagement.Interfaces.FareTransferRules;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.FareTransferRules;

namespace Byd.GtfsManagement.Managers.FareTransferRules;

// islevi: fare_transfer_rules.txt kurallarini korur.
// sistemdeki gorevi: Her kaydi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ucret aktarma kurali is kurallarini yonetir.</summary>
public class FareTransferRuleManager(
    IFareTransferRuleRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<FareTransferRule, FareTransferRuleCreateModel, FareTransferRuleUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni ucret aktarma kurali kaydini kurar.
    public override async Task<FareTransferRule> CreateAsync(
        FareTransferRuleCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new FareTransferRule(GuidGenerator.Create(), feed.Id, model.FareTransferType);
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut kaydin alanlarini gunceller.
    public override async Task UpdateAsync(
        FareTransferRule entity, FareTransferRuleUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.FareTransferType = model.FareTransferType;
        Apply(entity, model);
        await CheckUniqueAsync(entity, cancellationToken);
    }

    // Duzenlenebilir alanlari kirparak uygular.
    private static void Apply(FareTransferRule entity, FareTransferRuleInputModel model)
    {
        entity.FromLegGroupId = NormalizeOptionalText(model.FromLegGroupId);
        entity.ToLegGroupId = NormalizeOptionalText(model.ToLegGroupId);
        entity.TransferCount = model.TransferCount;
        entity.DurationLimit = model.DurationLimit;
        entity.DurationLimitType = model.DurationLimitType;
        entity.GtfsFareProductId = NormalizeOptionalText(model.GtfsFareProductId);
    }

    // Ayni Feed icinde tekilligi saglar.
    private Task CheckUniqueAsync(FareTransferRule entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var fromLegGroupId = entity.FromLegGroupId;
        var toLegGroupId = entity.ToLegGroupId;
        var transferCount = entity.TransferCount;
        var durationLimit = entity.DurationLimit;
        var durationLimitType = entity.DurationLimitType;
        var fareTransferType = entity.FareTransferType;
        var gtfsFareProductId = entity.GtfsFareProductId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.FromLegGroupId == fromLegGroupId && value.ToLegGroupId == toLegGroupId && value.TransferCount == transferCount && value.DurationLimit == durationLimit && value.DurationLimitType == durationLimitType && value.FareTransferType == fareTransferType && value.GtfsFareProductId == gtfsFareProductId,
            entity.Id, cancellationToken);
    }
}
