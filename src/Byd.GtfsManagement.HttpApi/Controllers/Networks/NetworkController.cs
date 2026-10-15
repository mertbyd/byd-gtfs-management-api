using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Networks;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Networks;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Networks;

// islevi: GTFS ulasim agi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Network isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ulasim agi HTTP uclarini sunar.</summary>
[ApiController]
[Route(NetworkRoutes.Root)]
public class NetworkController : GtfsManagementController
{
    private INetworkAppService AppService => LazyGetRequiredService<INetworkAppService>();

    /// <summary>Bir Feed'e yeni bir ulasim agi ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsNetworkId, NetworkName ve audit zamanlari alanlarini
    /// tasir. Feed istegin tenant kapsaminda degilse kayit olusturulmaz. Bu uc baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Networks.Manage)]
    public virtual async Task<Result<NetworkDto>> CreateAsync(
        [FromBody] CreateNetworkDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ulasim agi kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsNetworkId, NetworkName ve audit zamanlari alanlarini tasir. Kayit
    /// istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(NetworkRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Networks.View)]
    public virtual async Task<Result<NetworkDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis ulasim agi kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (NetworkFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId        (DataTableFilterItem?) → FeedId
    ///     GtfsNetworkId (DataTableFilterItem?) → GtfsNetworkId
    ///     NetworkName   (DataTableFilterItem?) → NetworkName
    ///     CreationTime  (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (NetworkDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(NetworkRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Networks.View)]
    public virtual async Task<Result<PagedResultDto<NetworkDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<NetworkFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Ulasim agi kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsNetworkId, NetworkName ve audit zamanlari alanlarini tasir.
    /// Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(NetworkRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Networks.Manage)]
    public virtual async Task<Result<NetworkDto>> UpdateAsync(
        Guid id, [FromBody] UpdateNetworkDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ulasim agi kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(NetworkRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Networks.Manage)]
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
    [Authorize(GtfsManagementPermissions.Networks.Manage)]
    public virtual async Task<Result<NetworkDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Networks.Manage)]
    public virtual async Task<Result<PagedResultDto<NetworkDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<NetworkFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
