using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Feeds;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Feeds;

// islevi: Feed yukleme, okuma, gecmis listeleme ve aktiflestirme islemlerinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Feed use case'lerini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(FeedRoutes.Root)]
public class FeedController : GtfsManagementController
{
    private IFeedAppService AppService => LazyGetRequiredService<IFeedAppService>();

    /// <summary>Aktif Feed'i istegin kapsaminda getirir.</summary>
    /// <param name="id">Okunacak Feed kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response value Feed alanlarini ve dogrulama raporunu tasir. Pasif Feed ve GTFS satirlari donmez.</remarks>
    [HttpGet(FeedRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Feeds.View)]
    public virtual async Task<Result<FeedDetailDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Yonetim icin pasif Feed'i de getirir.</summary>
    /// <param name="id">Okunacak Feed kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response value Feed alanlarini ve dogrulama raporunu tasir. Aktif Feed filtresi kontrollu kapanir; tenant ve organizasyon sinirlari korunur.</remarks>
    [HttpGet(FeedRoutes.IncludingInactive)]
    [Authorize(GtfsManagementPermissions.Feeds.Manage)]
    public virtual async Task<Result<FeedDetailDto>> GetIncludingInactiveAsync(
        Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetIncludingInactiveAsync(id, cancellationToken);

    /// <summary>Pasif ve basarisiz olanlar dahil tum Feed'leri yukleme gecmisi olarak getirir.</summary>
    /// <param name="input">Feed filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FeedFilter?)         → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     Name         (DataTableFilterItem?) → Feed adi filtresi
    ///     FeedId       (DataTableFilterItem?) → Feed kimligi
    ///     IsActive     (DataTableFilterItem?) → Yayin secimi filtresi
    ///     Status       (DataTableFilterItem?) → Durum filtresi
    ///     CreationTime (DataTableFilterItem?) → Yukleme zamani filtresi
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FeedDto[]) → Sayfadaki Feed'ler }
    /// Satirlar dogrulama raporunu tasimaz. Eslesen kayit yoksa toplam sayi sifir ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FeedRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Feeds.View)]
    public virtual async Task<Result<PagedResultDto<FeedDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FeedFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Entegre Feed'i organizasyonun aktif Feed'i yapar.</summary>
    /// <param name="id">Aktiflestirilecek Feed kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value Feed'in yeni aktiflik durumunu tasir. Yalniz Integrated ve hatasiz raporlu Feed kabul edilir.
    /// Ayni organizasyondaki onceki aktif Feed pasife alinir. GTFS satirlari degismez.
    /// </remarks>
    [HttpPost(FeedRoutes.Activate)]
    [Authorize(GtfsManagementPermissions.Feeds.Manage)]
    public virtual async Task<Result<FeedDto>> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.ActivateAsync(id, cancellationToken);

}
