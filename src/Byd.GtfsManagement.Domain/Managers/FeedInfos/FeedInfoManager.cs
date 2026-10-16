using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.FeedInfos;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.FeedInfos;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.FeedInfos;
using Volo.Abp;

namespace Byd.GtfsManagement.Managers.FeedInfos;

// islevi: feed_info.txt yayimci adi, url, dil, tarih ve tekillik kurallarini korur.
// sistemdeki gorevi: Her Feed altinda en fazla bir yayimci kaydi olmasini saglar.
/// <summary>GTFS yayimci bilgisi is kurallarini yonetir.</summary>
public class FeedInfoManager(
    IFeedInfoRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<FeedInfo, FeedInfoCreateModel, FeedInfoUpdateModel>(
        repository, feedManager)
{
    // Organizasyonun aktif Feed'inde yeni yayimci kaydini kurar.
    public override async Task<FeedInfo> CreateAsync(
        FeedInfoCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveActiveFeedAsync(cancellationToken);
        var entity = new FeedInfo(
            GuidGenerator.Create(), feed.Id);
        await ApplyAsync(entity, model, cancellationToken);
        return entity;
    }

    // Mevcut yayimci kaydinin alanlarini gunceller.
    public override Task UpdateAsync(
        FeedInfo entity, FeedInfoUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        return ApplyAsync(entity, model, cancellationToken);
    }

    // Yayimci metinlerini kirpar ve tarih sirasini dogrular.
    private async Task ApplyAsync(
        FeedInfo entity, FeedInfoInputModel model, CancellationToken cancellationToken = default)
    {
        entity.PublisherName = NormalizeRequiredText(model.PublisherName);
        entity.PublisherUrl = NormalizeRequiredText(model.PublisherUrl);
        entity.Language = NormalizeRequiredText(model.Language);
        entity.DefaultLanguage = NormalizeOptionalText(model.DefaultLanguage);
        entity.StartDate = NormalizeOptionalText(model.StartDate);
        entity.EndDate = NormalizeOptionalText(model.EndDate);
        ValidateDateOrder(entity.StartDate, entity.EndDate);
        entity.Version = NormalizeOptionalText(model.Version);
        entity.ContactEmail = NormalizeOptionalText(model.ContactEmail);
        entity.ContactUrl = NormalizeOptionalText(model.ContactUrl);

        await CheckFeedInfoUniqueAsync(entity, cancellationToken);
    }

    // Baslangic tarihinin bitis tarihinden once veya esit olmasini saglar.
    private static void ValidateDateOrder(string? startDate, string? endDate)
    {
        if (startDate is not null && endDate is not null && string.CompareOrdinal(startDate, endDate) > 0)
            throw new BusinessException(FeedInfoExceptionCodes.DateOrderInvalid);
    }

    // Ayni Feed altinda sadece tek bir yayimci kaydi olmasini saglar.
    private Task CheckFeedInfoUniqueAsync(FeedInfo entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId,
            entity.Id, cancellationToken);
    }
}
