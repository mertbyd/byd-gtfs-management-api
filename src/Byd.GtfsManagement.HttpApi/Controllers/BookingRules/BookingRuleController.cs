using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.BookingRules;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.BookingRules;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.BookingRules;

// islevi: GTFS rezervasyon kurali HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: BookingRule isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS rezervasyon kurali HTTP uclarini sunar.</summary>
[ApiController]
[Route(BookingRuleRoutes.Root)]
public class BookingRuleController : GtfsManagementController
{
    private IBookingRuleAppService AppService => LazyGetRequiredService<IBookingRuleAppService>();

    /// <summary>Bir Feed'e yeni bir rezervasyon kurali ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsBookingRuleId, PriorNoticeServiceId, BookingType,
    /// PriorNoticeDurationMin, PriorNoticeDurationMax, PriorNoticeLastDay, PriorNoticeLastTime,
    /// PriorNoticeStartDay, PriorNoticeStartTime, Message, PickupMessage, DropOffMessage, PhoneNumber, InfoUrl,
    /// BookingUrl ve audit zamanlari alanlarini tasir. Feed istegin tenant kapsaminda degilse kayit olusturulmaz.
    /// Bu uc baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.BookingRules.Manage)]
    public virtual async Task<Result<BookingRuleDto>> CreateAsync(
        [FromBody] CreateBookingRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen rezervasyon kurali kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsBookingRuleId, PriorNoticeServiceId, BookingType,
    /// PriorNoticeDurationMin, PriorNoticeDurationMax, PriorNoticeLastDay, PriorNoticeLastTime,
    /// PriorNoticeStartDay, PriorNoticeStartTime, Message, PickupMessage, DropOffMessage, PhoneNumber, InfoUrl,
    /// BookingUrl ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi hatasi
    /// doner.
    /// </remarks>
    [HttpGet(BookingRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.BookingRules.View)]
    public virtual async Task<Result<BookingRuleDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis rezervasyon kurali kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (BookingRuleFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId                 (DataTableFilterItem?) → FeedId
    ///     GtfsBookingRuleId      (DataTableFilterItem?) → GtfsBookingRuleId
    ///     BookingType            (DataTableFilterItem?) → BookingType
    ///     PriorNoticeDurationMin (DataTableFilterItem?) → PriorNoticeDurationMin
    ///     PriorNoticeDurationMax (DataTableFilterItem?) → PriorNoticeDurationMax
    ///     PriorNoticeLastDay     (DataTableFilterItem?) → PriorNoticeLastDay
    ///     PriorNoticeLastTime    (DataTableFilterItem?) → PriorNoticeLastTime
    ///     PriorNoticeStartDay    (DataTableFilterItem?) → PriorNoticeStartDay
    ///     PriorNoticeStartTime   (DataTableFilterItem?) → PriorNoticeStartTime
    ///     PriorNoticeServiceId   (DataTableFilterItem?) → PriorNoticeServiceId
    ///     Message                (DataTableFilterItem?) → Message
    ///     PickupMessage          (DataTableFilterItem?) → PickupMessage
    ///     DropOffMessage         (DataTableFilterItem?) → DropOffMessage
    ///     PhoneNumber            (DataTableFilterItem?) → PhoneNumber
    ///     InfoUrl                (DataTableFilterItem?) → InfoUrl
    ///     BookingUrl             (DataTableFilterItem?) → BookingUrl
    ///     CreationTime           (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (BookingRuleDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(BookingRuleRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.BookingRules.View)]
    public virtual async Task<Result<PagedResultDto<BookingRuleDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<BookingRuleFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Rezervasyon kurali kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsBookingRuleId, PriorNoticeServiceId, BookingType,
    /// PriorNoticeDurationMin, PriorNoticeDurationMax, PriorNoticeLastDay, PriorNoticeLastTime,
    /// PriorNoticeStartDay, PriorNoticeStartTime, Message, PickupMessage, DropOffMessage, PhoneNumber, InfoUrl,
    /// BookingUrl ve audit zamanlari alanlarini tasir. Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(BookingRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.BookingRules.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.BookingRulesUpdated,
        ActivityKind.Updated, nameof(BookingRuleDto.Id))]
    public virtual async Task<Result<BookingRuleDto>> UpdateAsync(
        Guid id, [FromBody] UpdateBookingRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Rezervasyon kurali kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(BookingRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.BookingRules.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.BookingRulesDeleted,
        ActivityKind.Deleted, nameof(BookingRuleDto.Id))]
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
    [Authorize(GtfsManagementPermissions.BookingRules.Manage)]
    public virtual async Task<Result<BookingRuleDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.BookingRules.Manage)]
    public virtual async Task<Result<PagedResultDto<BookingRuleDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<BookingRuleFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
