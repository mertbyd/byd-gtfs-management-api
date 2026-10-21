using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Timeframes;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Timeframes;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Timeframes;

// islevi: GTFS zaman araligi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Timeframe isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS zaman araligi HTTP uclarini sunar.</summary>
[ApiController]
[Route(TimeframeRoutes.Root)]
public class TimeframeController : GtfsManagementController
{
    private ITimeframeAppService AppService => LazyGetRequiredService<ITimeframeAppService>();

    /// <summary>Organizasyonun aktif Feed'ine yeni bir zaman araligi ekler.</summary>
    /// <param name="input">Kaydin GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, TimeframeGroupId, GtfsServiceId, StartTime, EndTime ve audit
    /// zamanlari alanlarini tasir. Organizasyonun aktif Feed'i yoksa kayit olusturulmaz. Bu uc baska bir GTFS kaydi
    /// olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Timeframes.Manage)]
    public virtual async Task<Result<TimeframeDto>> CreateAsync(
        [FromBody] CreateTimeframeDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen zaman araligi kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, TimeframeGroupId, GtfsServiceId, StartTime, EndTime ve audit zamanlari
    /// alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(TimeframeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Timeframes.View)]
    public virtual async Task<Result<TimeframeDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis zaman araligi kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (TimeframeFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId           (DataTableFilterItem?) → FeedId
    ///     TimeframeGroupId (DataTableFilterItem?) → TimeframeGroupId
    ///     StartTime        (DataTableFilterItem?) → StartTime
    ///     EndTime          (DataTableFilterItem?) → EndTime
    ///     GtfsServiceId    (DataTableFilterItem?) → GtfsServiceId
    ///     CreationTime     (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (TimeframeDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(TimeframeRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Timeframes.View)]
    public virtual async Task<Result<PagedResultDto<TimeframeDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<TimeframeFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Zaman araligi kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, TimeframeGroupId, GtfsServiceId, StartTime, EndTime ve audit
    /// zamanlari alanlarini tasir. Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(TimeframeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Timeframes.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.TimeframesUpdated,
        ActivityKind.Updated, nameof(TimeframeDto.Id))]
    public virtual async Task<Result<TimeframeDto>> UpdateAsync(
        Guid id, [FromBody] UpdateTimeframeDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Zaman araligi kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(TimeframeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Timeframes.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.TimeframesDeleted,
        ActivityKind.Deleted, nameof(TimeframeDto.Id))]
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
    [Authorize(GtfsManagementPermissions.Timeframes.Manage)]
    public virtual async Task<Result<TimeframeDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Timeframes.Manage)]
    public virtual async Task<Result<PagedResultDto<TimeframeDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<TimeframeFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
