using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FareAttributes;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.FareAttributes;
using Byd.GtfsManagement.Managers.Agencies;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.FareAttributes;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.FareAttributes;

// islevi: fare_attributes.txt ucret kimligi, fiyat, para birimi, odeme modu ve ajans kurallarini korur.
// sistemdeki gorevi: Her ucret sinifini Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ucret ozelligi is kurallarini yonetir.</summary>
public class FareAttributeManager(
    IFareAttributeRepository repository,
    FeedManager feedManager,
    AgencyManager agencyManager)
    : GtfsFeedScopedManager<FareAttribute, FareAttributeCreateModel, FareAttributeUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni ucret ozelligi kaydini kurar.
    public override async Task<FareAttribute> CreateAsync(
        FareAttributeCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new FareAttribute(
            GuidGenerator.Create(), feed.Id, NormalizeRequiredText(model.GtfsFareId));
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut ucretin fiyat, para birimi ve aktarma alanlarini gunceller.
    public override Task UpdateAsync(
        FareAttribute entity, FareAttributeUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.GtfsFareId = NormalizeRequiredText(model.GtfsFareId);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Opsiyonel ajans bagini dogrular, para birimini buyuk harfe cevirir ve alanlari uygular.
    private async Task ApplyAsync(
        FareAttribute entity, FareAttributeInputModel model, CancellationToken cancellationToken = default)
    {
        await ValidateAgencyAsync(entity, model.AgencyId, cancellationToken);

        entity.Price = model.Price;
        entity.CurrencyType = NormalizeRequiredText(model.CurrencyType).ToUpperInvariant();
        entity.PaymentMethod = model.PaymentMethod;
        entity.Transfers = model.Transfers;
        entity.AgencyId = model.AgencyId;
        entity.TransferDuration = model.TransferDuration;

        await CheckFareUniqueAsync(entity, cancellationToken);
    }

    // Ajans secilmisse ayni Feed icinde oldugunu dogrular.
    private async Task ValidateAgencyAsync(
        FareAttribute entity, Guid? agencyId, CancellationToken cancellationToken = default)
    {
        if (!agencyId.HasValue) return;
        var agency = await agencyManager.EnsureExistsInScopeAsync(agencyId.Value, cancellationToken);
        if (agency.FeedId != entity.FeedId)
            throw new BusinessException(FareAttributeExceptionCodes.AgencyOutOfFeed);
    }

    // Ayni Feed icinde fare_id tekilligini saglar.
    private Task CheckFareUniqueAsync(FareAttribute entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var gtfsFareId = entity.GtfsFareId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId && value.GtfsFareId == gtfsFareId,
            entity.Id, cancellationToken);
    }
}
