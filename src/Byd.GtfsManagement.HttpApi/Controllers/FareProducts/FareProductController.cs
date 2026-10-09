using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareProducts;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.FareProducts;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.FareProducts;

// islevi: GTFS ucret urunu HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: FareProduct isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ucret urunu HTTP uclarini sunar.</summary>
[ApiController]
[Route(FareProductRoutes.Root)]
public class FareProductController : GtfsManagementController
{
    private IFareProductAppService AppService => LazyGetRequiredService<IFareProductAppService>();

    /// <summary>Bir Feed'e yeni bir ucret urunu ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsFareProductId, GtfsRiderCategoryId, GtfsFareMediaId,
    /// FareProductName, Amount, Currency ve audit zamanlari alanlarini tasir. Feed istegin tenant kapsaminda
    /// degilse kayit olusturulmaz. Bu uc baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.FareProducts.Manage)]
    public virtual async Task<Result<FareProductDto>> CreateAsync(
        [FromBody] CreateFareProductDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ucret urunu kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsFareProductId, GtfsRiderCategoryId, GtfsFareMediaId, FareProductName,
    /// Amount, Currency ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi
    /// hatasi doner.
    /// </remarks>
    [HttpGet(FareProductRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareProducts.View)]
    public virtual async Task<Result<FareProductDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis ucret urunu kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FareProductFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId              (DataTableFilterItem?) → FeedId
    ///     GtfsFareProductId   (DataTableFilterItem?) → GtfsFareProductId
    ///     FareProductName     (DataTableFilterItem?) → FareProductName
    ///     GtfsRiderCategoryId (DataTableFilterItem?) → GtfsRiderCategoryId
    ///     GtfsFareMediaId     (DataTableFilterItem?) → GtfsFareMediaId
    ///     Amount              (DataTableFilterItem?) → Amount
    ///     Currency            (DataTableFilterItem?) → Currency
    ///     CreationTime        (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FareProductDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FareProductRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.FareProducts.View)]
    public virtual async Task<Result<PagedResultDto<FareProductDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FareProductFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Ucret urunu kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsFareProductId, GtfsRiderCategoryId, GtfsFareMediaId,
    /// FareProductName, Amount, Currency ve audit zamanlari alanlarini tasir. Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(FareProductRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareProducts.Manage)]
    public virtual async Task<Result<FareProductDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFareProductDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ucret urunu kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(FareProductRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareProducts.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }
}
