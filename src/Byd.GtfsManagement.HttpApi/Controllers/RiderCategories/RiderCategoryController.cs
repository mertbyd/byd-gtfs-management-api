using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.RiderCategories;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.RiderCategories;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.RiderCategories;

// islevi: GTFS yolcu kategorisi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: RiderCategory isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS yolcu kategorisi HTTP uclarini sunar.</summary>
[ApiController]
[Route(RiderCategoryRoutes.Root)]
public class RiderCategoryController : GtfsManagementController
{
    private IRiderCategoryAppService AppService => LazyGetRequiredService<IRiderCategoryAppService>();

    /// <summary>Organizasyonun aktif Feed'ine yeni bir yolcu kategorisi ekler.</summary>
    /// <param name="input">Kaydin GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsRiderCategoryId, RiderCategoryName, IsDefaultFareCategory,
    /// EligibilityUrl ve audit zamanlari alanlarini tasir. Organizasyonun aktif Feed'i yoksa kayit olusturulmaz. Bu uc
    /// baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.RiderCategories.Manage)]
    public virtual async Task<Result<RiderCategoryDto>> CreateAsync(
        [FromBody] CreateRiderCategoryDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen yolcu kategorisi kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsRiderCategoryId, RiderCategoryName, IsDefaultFareCategory,
    /// EligibilityUrl ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi
    /// hatasi doner.
    /// </remarks>
    [HttpGet(RiderCategoryRoutes.ById)]
    [Authorize(GtfsManagementPermissions.RiderCategories.View)]
    public virtual async Task<Result<RiderCategoryDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis yolcu kategorisi kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (RiderCategoryFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId                (DataTableFilterItem?) → FeedId
    ///     GtfsRiderCategoryId   (DataTableFilterItem?) → GtfsRiderCategoryId
    ///     RiderCategoryName     (DataTableFilterItem?) → RiderCategoryName
    ///     IsDefaultFareCategory (DataTableFilterItem?) → IsDefaultFareCategory
    ///     EligibilityUrl        (DataTableFilterItem?) → EligibilityUrl
    ///     CreationTime          (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (RiderCategoryDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(RiderCategoryRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.RiderCategories.View)]
    public virtual async Task<Result<PagedResultDto<RiderCategoryDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<RiderCategoryFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Yolcu kategorisi kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsRiderCategoryId, RiderCategoryName, IsDefaultFareCategory,
    /// EligibilityUrl ve audit zamanlari alanlarini tasir. Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(RiderCategoryRoutes.ById)]
    [Authorize(GtfsManagementPermissions.RiderCategories.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.RiderCategoriesUpdated,
        ActivityKind.Updated, nameof(RiderCategoryDto.Id))]
    public virtual async Task<Result<RiderCategoryDto>> UpdateAsync(
        Guid id, [FromBody] UpdateRiderCategoryDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Yolcu kategorisi kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(RiderCategoryRoutes.ById)]
    [Authorize(GtfsManagementPermissions.RiderCategories.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.RiderCategoriesDeleted,
        ActivityKind.Deleted, nameof(RiderCategoryDto.Id))]
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
    [Authorize(GtfsManagementPermissions.RiderCategories.Manage)]
    public virtual async Task<Result<RiderCategoryDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.RiderCategories.Manage)]
    public virtual async Task<Result<PagedResultDto<RiderCategoryDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<RiderCategoryFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
