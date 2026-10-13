using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Stops;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Stops;

// islevi: stops.txt islemlerinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Stop use case'lerini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(StopRoutes.Root)]
public class StopController : GtfsManagementController
{
    private IStopAppService AppService => LazyGetRequiredService<IStopAppService>();

    /// <summary>Hedef GTFS Feed'e yeni durak kaydi ekler.</summary>
    /// <param name="input">Duragin yazilacagi Feed ve stops.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value durak kimligini, Feed kimligini, konum turu ve erisim lookup kimlikleri ile adlarini,
    /// ust istasyon ve kat kimlikleri ile adlarini, GTFS alanlarini ve audit zamanlarini tasir. Durak, istasyon ve giris
    /// kayitlari icin ad ile koordinat zorunludur; giris, ara dugum ve binis alani kayitlari bir istasyona
    /// baglanmalidir. Bu endpoint stop_times veya transfer kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Stops.Manage)]
    public virtual async Task<Result<StopDto>> CreateAsync(
        [FromBody] CreateStopDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen duragi istegin kapsaminda getirir.</summary>
    /// <param name="id">Okunacak Stop kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value durak kimligini, Feed kimligini, lookup kimlikleri ile adlarini, ust istasyon ve kat kimlikleri ile
    /// adlarini, GTFS alanlarini ve audit zamanlarini tasir. Duragin alt kayitlari ve sefer saatleri okunmaz.
    /// </remarks>
    [HttpGet(StopRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Stops.View)]
    public virtual async Task<Result<StopDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtre, siralama ve sayfalama uygulanmis duraklari getirir.</summary>
    /// <param name="input">Durak filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (StopFilter?)         → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId        (DataTableFilterItem?) → Feed filtresi
    ///     LocationTypeId       (DataTableFilterItem?) → Konum turu filtresi
    ///     WheelchairBoardingId (DataTableFilterItem?) → Erisim durumu filtresi
    ///     ParentStationId      (DataTableFilterItem?) → Ust istasyon filtresi
    ///     GtfsStopId           (DataTableFilterItem?) → GTFS stop_id filtresi
    ///     Code                 (DataTableFilterItem?) → Durak kodu filtresi
    ///     Name                 (DataTableFilterItem?) → Durak adi filtresi
    ///     ZoneId               (DataTableFilterItem?) → Ucret bolgesi filtresi
    ///     CreationTime         (DataTableFilterItem?) → Kayit zamani filtresi
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (StopDto[]) → Sayfadaki duraklar }
    /// Sayfa yalnizca istegin tenant'indaki duraklari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(StopRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Stops.View)]
    public virtual async Task<Result<PagedResultDto<StopDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<StopFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Kimligi verilen duragin GTFS alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek Stop kaydinin kimligi.</param>
    /// <param name="input">Duragin yeni stops.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel durak alanlarini tasir. Kaydin bagli oldugu Feed degistirilemez; ust istasyon ayni
    /// Feed'de ve konum turu istasyon olan bir kayit olmalidir.
    /// </remarks>
    [HttpPut(StopRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Stops.Manage)]
    public virtual async Task<Result<StopDto>> UpdateAsync(
        Guid id, [FromBody] UpdateStopDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Kimligi verilen duragi siler.</summary>
    /// <param name="id">Silinecek Stop kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Govde dondurmez. Silme kalicidir; duraga bagli alt konumlar veya sefer saatleri varsa iliski kurali
    /// silmeyi engeller.
    /// </remarks>
    [HttpDelete(StopRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Stops.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }

    /// <summary>Verilen ya da tenant'in aktif GTFS Feed'indeki tum duraklari harita icin getirir.</summary>
    /// <param name="feedId">Duraklari okunacak Feed; verilmezse tenant'in aktif Feed'i kullanilir.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   feedId (Guid?) → Okunacak Feed (query); bos ise aktif Feed
    /// }
    /// Response elemanlari filtreli liste ucundaki StopDto alanlarini tasir: konum turu, erisim, ust istasyon ve kat
    /// kimlikleri ile adlari, koordinatlar. Liste sayfalanmaz; tenant'in aktif Feed'i yoksa bos liste doner. Bu endpoint
    /// sefer saatlerini okumaz ve kayit yazmaz.
    /// </remarks>
    [HttpGet(StopRoutes.Map)]
    [Authorize(GtfsManagementPermissions.Stops.View)]
    public virtual async Task<Result<List<StopDto>>> GetMapAsync(
        [FromQuery] Guid? feedId, CancellationToken cancellationToken = default)
        => await AppService.GetMapAsync(feedId, cancellationToken);

    /// <summary>Duragi alt peronlari, ugrayan guzergahlari, pathway ve transfer kayitlariyla birlikte getirir.</summary>
    /// <param name="id">Detayi okunacak Stop kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value Stop alaninda durak kaydini; Children alaninda istasyona bagli peron, giris ve dugumleri; Routes
    /// alaninda duraga veya peronlarina ugrayan guzergahlari RouteDto olarak; Pathways ve Transfers alanlarinda bu
    /// duraklara bagli istasyon ici yollari ve aktarma kurallarini durak adlariyla tasir. Sefer saatleri bu endpointte
    /// okunmaz; kalkislar icin departures ucu kullanilir.
    /// </remarks>
    [HttpGet(StopRoutes.Detail)]
    [Authorize(GtfsManagementPermissions.Stops.View)]
    public virtual async Task<Result<StopDetailDto>> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetDetailAsync(id, cancellationToken);

    /// <summary>Duraktan ve istasyonsa peronlarindan servis gununde verilen saatten sonra kalkan seferleri getirir.</summary>
    /// <param name="id">Kalkislari okunacak Stop kaydinin kimligi.</param>
    /// <param name="input">Servis gunu, baslangic saati ve kalkis siniri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Date     (DateOnly) → GTFS servis gunu (yyyy-MM-dd)
    ///   FromTime (string?)  → Bu saatten sonraki kalkislar (HH:MM:SS); bos ise gun basi
    ///   Limit    (int)      → En fazla kalkis sayisi; varsayilan 20, en fazla 200
    /// }
    /// Response elemanlari StopTimeDto alanlarina (sefer ve kalkilan peron kimlik-adlari, sira, saatler, durak tabelasi)
    /// ek olarak guzergahin kimligini, adini, renklerini ve sefer tabelasini tasir; kalkis saatine gore siralidir.
    /// Servis gunu GTFS anlamindadir: gece yarisini asan seferler kendi servis gununde 24:00 sonrasi saatle listelenir.
    /// O gun calisan servis yoksa bos liste doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(StopRoutes.Departures)]
    [Authorize(GtfsManagementPermissions.Stops.View)]
    public virtual async Task<Result<List<StopDepartureDto>>> GetDeparturesAsync(
        Guid id, [FromQuery] StopDepartureInputDto input, CancellationToken cancellationToken = default)
        => await AppService.GetDeparturesAsync(id, input, cancellationToken);
}
