using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.RouteNetworks;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.RouteNetworks;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.RouteNetworks;

// islevi: GTFS hat ag iliskisi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: RouteNetwork isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS hat ag iliskisi HTTP uclarini sunar.</summary>
[ApiController]
[Route(RouteNetworkRoutes.Root)]
public class RouteNetworkController : GtfsManagementController
{
    private IRouteNetworkAppService AppService => LazyGetRequiredService<IRouteNetworkAppService>();

    /// <summary>Bir Feed'e yeni bir hat ag iliskisi ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsNetworkId, GtfsRouteId ve audit zamanlari alanlarini
    /// tasir. Feed istegin tenant kapsaminda degilse kayit olusturulmaz. Bu uc baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.RouteNetworks.Manage)]
    public virtual async Task<Result<RouteNetworkDto>> CreateAsync(
        [FromBody] CreateRouteNetworkDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen hat ag iliskisi kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsNetworkId, GtfsRouteId ve audit zamanlari alanlarini tasir. Kayit
    /// istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(RouteNetworkRoutes.ById)]
    [Authorize(GtfsManagementPermissions.RouteNetworks.View)]
    public virtual async Task<Result<RouteNetworkDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis hat ag iliskisi kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (RouteNetworkFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId        (DataTableFilterItem?) → FeedId
    ///     GtfsNetworkId (DataTableFilterItem?) → GtfsNetworkId
    ///     GtfsRouteId   (DataTableFilterItem?) → GtfsRouteId
    ///     CreationTime  (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (RouteNetworkDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(RouteNetworkRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.RouteNetworks.View)]
    public virtual async Task<Result<PagedResultDto<RouteNetworkDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<RouteNetworkFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Hat ag iliskisi kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsNetworkId, GtfsRouteId ve audit zamanlari alanlarini tasir.
    /// Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(RouteNetworkRoutes.ById)]
    [Authorize(GtfsManagementPermissions.RouteNetworks.Manage)]
    public virtual async Task<Result<RouteNetworkDto>> UpdateAsync(
        Guid id, [FromBody] UpdateRouteNetworkDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Hat ag iliskisi kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(RouteNetworkRoutes.ById)]
    [Authorize(GtfsManagementPermissions.RouteNetworks.Manage)]
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
    [Authorize(GtfsManagementPermissions.RouteNetworks.Manage)]
    public virtual async Task<Result<RouteNetworkDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.RouteNetworks.Manage)]
    public virtual async Task<Result<PagedResultDto<RouteNetworkDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<RouteNetworkFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
