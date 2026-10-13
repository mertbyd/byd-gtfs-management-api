using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Translations;
using Byd.GtfsManagement.Interfaces.Translations;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Models.Translations;

namespace Byd.GtfsManagement.Managers.Translations;

// islevi: translations.txt tablo, alan, dil, hedef kayit ve ceviri metni kurallarini korur.
// sistemdeki gorevi: Her ceviriyi Feed kapsaminda yonetir ve tekilligi saglar.
/// <summary>GTFS ceviri is kurallarini yonetir.</summary>
public class TranslationManager(
    ITranslationRepository repository,
    FeedManager feedManager)
    : GtfsFeedScopedManager<Translation, TranslationCreateModel, TranslationUpdateModel>(
        repository, feedManager)
{
    // Feed kapsamindan yeni ceviri kaydini kurar.
    public override async Task<Translation> CreateAsync(
        TranslationCreateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        var feed = await ResolveFeedAsync(model.FeedId, cancellationToken);
        var entity = new Translation(
            GuidGenerator.Create(), feed.TenantId,
            feed.Id, NormalizeRequiredText(model.TableName), NormalizeRequiredText(model.FieldName),
            NormalizeRequiredText(model.Language), NormalizeRequiredText(model.TranslationText));
        ApplyTargets(entity, model);
        await CheckTranslationUniqueAsync(entity, cancellationToken);
        return entity;
    }

    // Mevcut cevirinin metin veya hedef alanlarini gunceller.
    public override async Task UpdateAsync(
        Translation entity, TranslationUpdateModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        entity.TableName = NormalizeRequiredText(model.TableName);
        entity.FieldName = NormalizeRequiredText(model.FieldName);
        entity.Language = NormalizeRequiredText(model.Language);
        entity.TranslationText = NormalizeRequiredText(model.TranslationText);
        ApplyTargets(entity, model);
        await CheckTranslationUniqueAsync(entity, cancellationToken);
    }

    // Hedef kayit ve deger alanlarini kirpar.
    private static void ApplyTargets(Translation entity, TranslationInputModel model)
    {
        entity.RecordId = NormalizeOptionalText(model.RecordId);
        entity.RecordSubId = NormalizeOptionalText(model.RecordSubId);
        entity.FieldValue = NormalizeOptionalText(model.FieldValue);
    }

    // Ayni Feed icinde ceviri tekilligini saglar.
    private Task CheckTranslationUniqueAsync(Translation entity, CancellationToken cancellationToken = default)
    {
        var feedId = entity.FeedId;
        var table = entity.TableName;
        var field = entity.FieldName;
        var lang = entity.Language;
        var recordId = entity.RecordId;
        var recordSubId = entity.RecordSubId;
        var fieldValue = entity.FieldValue;

        return CheckAlreadyExistsAsync(
            value => value.FeedId == feedId
                  && value.TableName == table
                  && value.FieldName == field
                  && value.Language == lang
                  && value.RecordId == recordId
                  && value.RecordSubId == recordSubId
                  && value.FieldValue == fieldValue,
            entity.Id, cancellationToken);
    }
}
