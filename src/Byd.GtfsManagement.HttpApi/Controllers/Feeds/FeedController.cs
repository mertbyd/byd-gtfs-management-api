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

    /// <summary>GTFS ZIP'ini yukler, dogrular ve dogrulamayi gecerse veritabanina aktarilmak uzere kuyruga birakir.</summary>
    /// <param name="input">Opsiyonel Feed adi, ZIP dosya adi ve dosya icerigi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Name     (string?) → Ad verilmezse dosya adi, zaman ve organizasyon kimliginden uretilir
    ///   FileName (string) → .zip uzantili GTFS dosyasinin adi
    ///   Data     (byte[]) → ZIP icerigi; JSON'da base64
    /// }
    /// ZIP dogrulamadan once file modulune kaydedilir ve Feed pasif olarak acilir; dogrulayiciya ulasilamazsa Feed Uploaded
    /// durumunda kalir. Response value Feed kimligini, dosyasini, durumunu (ValidationFailed veya Validated), hata ve uyari
    /// sayilarini ve dogrulama raporunu tasir. Validated Feed'in GTFS tablolari arka planda yazilir; bu endpoint tablolara
    /// yazmaz ve Feed'i aktiflestirmez.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Feeds.Manage)]
    public virtual async Task<Result<FeedDetailDto>> UploadAsync(
        [FromBody] UploadFeedDto input, CancellationToken cancellationToken = default)
        => await AppService.UploadAsync(input, cancellationToken);

    /// <summary>URL'den ZIP'i ceker ve yeni pasif Feed olarak kaydeder.</summary>
    /// <param name="input">HTTP(S) kaynak URL'si ve opsiyonel Feed adi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request { Url (string), Name (string?) }. Kaynak adresi yeni Feed'in SourceUrl alaninda saklanir.
    /// Yerel veya ozel ag hedefi reddedilir; cagri dis servis loguna yazilir.
    /// Response value yukleme ucuyla aynidir; yeni Feed yayin secimini degistirmez.
    /// </remarks>
    [HttpPost(FeedRoutes.ImportFromSource)]
    [Authorize(GtfsManagementPermissions.Feeds.Manage)]
    public virtual async Task<Result<FeedDetailDto>> ImportFromSourceAsync(
        [FromBody] ImportFeedDto input, CancellationToken cancellationToken = default)
        => await AppService.ImportFromSourceAsync(input, cancellationToken);

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

    /// <summary>Feed'in guncel GTFS verisini yeniden dogrular.</summary>
    /// <param name="id">Dogrulanacak Feed kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response value dosya, alan ve satir ayrintilarini iceren rapordur. Bu endpoint Feed'i aktifleştirmez.</remarks>
    [HttpPost(FeedRoutes.Validate)]
    [Authorize(GtfsManagementPermissions.Feeds.Manage)]
    public virtual async Task<Result<FeedValidationReportDto>> ValidateAsync(
        Guid id, CancellationToken cancellationToken = default)
        => await AppService.ValidateAsync(id, cancellationToken);

    /// <summary>Feed'in kayitli dogrulama raporunu JSON dosyasi olarak verir.</summary>
    /// <param name="id">Raporu indirilecek Feed kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response value dosya adini, JSON icerik turunu ve dosya baytlarini tasir; GTFS ZIP'i tasimaz.</remarks>
    [HttpGet(FeedRoutes.ValidationReport)]
    [Authorize(GtfsManagementPermissions.Feeds.Export)]
    public virtual async Task<Result<FeedFileDto>> ExportValidationReportAsync(
        Guid id, CancellationToken cancellationToken = default)
        => await AppService.ExportValidationReportAsync(id, cancellationToken);

    /// <summary>Feed'in temel GTFS tablolarindaki kayit sayilarini ve servis tarih araligini getirir.</summary>
    /// <param name="id">Ozeti okunacak Feed kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value Feed alaninda Feed satirini; ajans, durak, guzergah, sefer, durak zamani, servis ve shape sayilarini; takvim ve
    /// tarih istisnalarinin kapsadigi ilk ve son servis gununu tasir. Pasif Feed de okunur. Dogrulama raporu bu cevapta yoktur.
    /// </remarks>
    [HttpGet(FeedRoutes.Summary)]
    [Authorize(GtfsManagementPermissions.Feeds.View)]
    public virtual async Task<Result<FeedSummaryDto>> GetSummaryAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetSummaryAsync(id, cancellationToken);
    /// <summary>Entegre Feed'in guncel GTFS dosyalarini ZIP olarak verir.</summary>
    /// <param name="id">Disa aktarilacak Feed kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response value ZIP dosya adini, icerik turunu ve baytlarini tasir. Kayitli orijinal ZIP yerine guncel tablolar kullanilir.</remarks>
    [HttpGet(FeedRoutes.ExportGtfs)]
    [Authorize(GtfsManagementPermissions.Feeds.Export)]
    public virtual async Task<Result<FeedFileDto>> ExportGtfsAsync(
        Guid id, CancellationToken cancellationToken = default)
        => await AppService.ExportGtfsAsync(id, cancellationToken);
}
