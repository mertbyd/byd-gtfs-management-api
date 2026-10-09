using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
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
}
