using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Areas;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Areas;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Areas;

// islevi: GTFS bolge HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Area isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS bolge HTTP uclarini sunar.</summary>
[ApiController]
[Route(AreaRoutes.Root)]
public class AreaController : GtfsManagementController
{
    private IAreaAppService AppService => LazyGetRequiredService<IAreaAppService>();

    /// <summary>Organizasyonun aktif Feed'ine yeni bir bolge ekler.</summary>
    /// <param name="input">Bolge alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsAreaId, AreaName ve audit zamanlari alanlarini tasir. Ayni
    /// GTFS bolge kimligi ayni Feed'de tekrar edemez.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Areas.Manage)]
    public virtual async Task<Result<AreaDto>> CreateAsync(
        [FromBody] CreateAreaDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen bolgeyi getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsAreaId, AreaName ve audit zamanlari alanlarini tasir. Kayit istegin
    /// tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(AreaRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Areas.View)]
    public virtual async Task<Result<AreaDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis bolgeleri sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (AreaFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId       (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     GtfsAreaId   (DataTableFilterItem?) → GTFS bolge kimligine gore filtreler
    ///     AreaName     (DataTableFilterItem?) → Bolge adina gore filtreler
    ///     CreationTime (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (AreaDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(AreaRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Areas.View)]
    public virtual async Task<Result<PagedResultDto<AreaDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<AreaFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Bolgenin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni bolge alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsAreaId, AreaName ve audit zamanlari alanlarini tasir. Bagli
    /// Feed degistirilemez.
    /// </remarks>
    [HttpPut(AreaRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Areas.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.AreasUpdated,
        ActivityKind.Updated, nameof(AreaDto.Id))]
    public virtual async Task<Result<AreaDto>> UpdateAsync(
        Guid id, [FromBody] UpdateAreaDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Bolgeyi kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; bolge kaydi kalici olarak silinir.</remarks>
    [HttpDelete(AreaRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Areas.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.AreasDeleted,
        ActivityKind.Deleted, nameof(AreaDto.Id))]
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
    [Authorize(GtfsManagementPermissions.Areas.Manage)]
    public virtual async Task<Result<AreaDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Areas.Manage)]
    public virtual async Task<Result<PagedResultDto<AreaDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<AreaFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
