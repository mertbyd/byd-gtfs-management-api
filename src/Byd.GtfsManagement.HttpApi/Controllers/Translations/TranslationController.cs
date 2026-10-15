using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Translations;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Translations;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Translations;

// islevi: GTFS ceviri HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Translation isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ceviri HTTP uclarini sunar.</summary>
[ApiController]
[Route(TranslationRoutes.Root)]
public class TranslationController : GtfsManagementController
{
    private ITranslationAppService AppService => LazyGetRequiredService<ITranslationAppService>();

    /// <summary>Bir Feed'e yeni bir ceviri ekler.</summary>
    /// <param name="input">Feed, tablo, alan, dil ve hedef/deger alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, RecordId, RecordSubId, TableName, FieldName, Language,
    /// TranslationText, FieldValue ve audit zamanlari alanlarini tasir. Ayni tablo, alan, dil ve hedef kayit/deger
    /// kombinasyonu ayni Feed'de tekrar edemez.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Translations.Manage)]
    public virtual async Task<Result<TranslationDto>> CreateAsync(
        [FromBody] CreateTranslationDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ceviriyi getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, RecordId, RecordSubId, TableName, FieldName, Language, TranslationText,
    /// FieldValue ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi hatasi
    /// doner.
    /// </remarks>
    [HttpGet(TranslationRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Translations.View)]
    public virtual async Task<Result<TranslationDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis cevirileri sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (TranslationFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId          (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     TableName       (DataTableFilterItem?) → Tablo adina gore filtreler
    ///     FieldName       (DataTableFilterItem?) → Alan adina gore filtreler
    ///     Language        (DataTableFilterItem?) → Dil koduna gore filtreler
    ///     RecordId        (DataTableFilterItem?) → Dis kayit kimligine gore filtreler
    ///     RecordSubId     (DataTableFilterItem?) → Alt kayit kimligine gore filtreler
    ///     FieldValue      (DataTableFilterItem?) → Kaynak deger metnine gore filtreler
    ///     TranslationText (DataTableFilterItem?) → Ceviri metnine gore filtreler
    ///     CreationTime    (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (TranslationDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(TranslationRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Translations.View)]
    public virtual async Task<Result<PagedResultDto<TranslationDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<TranslationFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Cevirinin metin veya hedef alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni tablo, alan, dil veya metin alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, RecordId, RecordSubId, TableName, FieldName, Language,
    /// TranslationText, FieldValue ve audit zamanlari alanlarini tasir. Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(TranslationRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Translations.Manage)]
    public virtual async Task<Result<TranslationDto>> UpdateAsync(
        Guid id, [FromBody] UpdateTranslationDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ceviriyi kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; ceviri kaydi kalici olarak silinir.</remarks>
    [HttpDelete(TranslationRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Translations.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }

    /// <summary>Belirli surumun taslak veya gecmis kaydini getirir.</summary>
    /// <param name="feedId">Kapsamda okunacak surum kimligi.</param>
    /// <param name="id">Okunacak kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap yalniz istenen surumun kaydini tasir; tenant ve organizasyon sinirlari korunur.</remarks>
    [HttpGet(GtfsRecordRoutes.ForFeed)]
    [Authorize(GtfsManagementPermissions.Translations.Manage)]
    public virtual async Task<Result<TranslationDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Translations.Manage)]
    public virtual async Task<Result<PagedResultDto<TranslationDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<TranslationFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
