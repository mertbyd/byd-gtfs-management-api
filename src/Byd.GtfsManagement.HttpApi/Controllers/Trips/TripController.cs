using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Trips;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Trips;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Trips;

// islevi: trips.txt islemlerinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Trip use case'lerini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(TripRoutes.Root)]
public class TripController : GtfsManagementController
{
    private ITripAppService AppService => LazyGetRequiredService<ITripAppService>();

    /// <summary>Hedef GTFS Feed'e yeni sefer kaydi ekler.</summary>
    /// <param name="input">Seferin yazilacagi Feed ve trips.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value sefer kimligini, Feed kimligini, guzergah ve servis kimlikleri ile GTFS kimliklerini,
    /// lookup kimlikleri ile adlarini, GTFS alanlarini ve audit zamanlarini tasir. Guzergah ile servis seferle
    /// ayni Feed'de olmalidir ve trip_id Feed icinde tekildir. Bu endpoint stop_times kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Trips.Manage)]
    public virtual async Task<Result<TripDto>> CreateAsync(
        [FromBody] CreateTripDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen seferi istegin kapsaminda getirir.</summary>
    /// <param name="id">Okunacak Trip kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value sefer kimligini, Feed kimligini, guzergah ve servis kimlikleri ile GTFS kimliklerini,
    /// lookup kimlikleri ile adlarini, GTFS alanlarini ve audit zamanlarini tasir. Seferin durak zamanlari bu
    /// endpointte okunmaz.
    /// </remarks>
    [HttpGet(TripRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Trips.View)]
    public virtual async Task<Result<TripDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtre, siralama ve sayfalama uygulanmis seferleri getirir.</summary>
    /// <param name="input">Sefer filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (TripFilter?)         → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId    (DataTableFilterItem?) → Feed filtresi
    ///     RouteId          (DataTableFilterItem?) → Guzergah filtresi
    ///     ServiceId        (DataTableFilterItem?) → Servis filtresi
    ///     TripDirectionId  (DataTableFilterItem?) → Yon filtresi
    ///     GtfsTripId       (DataTableFilterItem?) → GTFS trip_id filtresi
    ///     Headsign         (DataTableFilterItem?) → Varis tabelasi filtresi
    ///     BlockId          (DataTableFilterItem?) → Blok kimligi filtresi
    ///     CreationTime     (DataTableFilterItem?) → Kayit zamani filtresi
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (TripDto[]) → Sayfadaki seferler }
    /// Sayfa yalnizca istegin tenant'indaki seferleri tasir. Eslesen kayit yoksa toplam sayi
    /// sifir ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(TripRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Trips.View)]
    public virtual async Task<Result<PagedResultDto<TripDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<TripFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Kimligi verilen seferin GTFS alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek Trip kaydinin kimligi.</param>
    /// <param name="input">Seferin yeni trips.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel sefer alanlarini tasir. Kaydin bagli oldugu Feed degistirilemez; secilen guzergah
    /// ve servis ayni Feed'de olmalidir.
    /// </remarks>
    [HttpPut(TripRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Trips.Manage)]
    public virtual async Task<Result<TripDto>> UpdateAsync(
        Guid id, [FromBody] UpdateTripDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Kimligi verilen seferi siler.</summary>
    /// <param name="id">Silinecek Trip kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Govde dondurmez. Silme kalicidir; sefere bagli durak zamanlari varsa iliski kurali silmeyi engeller.
    /// </remarks>
    [HttpDelete(TripRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Trips.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }

    /// <summary>Seferi duraklari, sikliklari, sekli ve servisinin calisma gunleriyle birlikte getirir.</summary>
    /// <param name="id">Detayi okunacak Trip kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value Trip alaninda sefer kaydini; Stops alaninda durak sirasini ad, koordinat, varis ve kalkis saatleriyle;
    /// Frequencies alaninda frequencies.txt araliklarini; Shape alaninda varsa sekil noktalarini; Calendar ve CalendarDates
    /// alanlarinda servisin haftalik kuralini ve tarih istisnalarini tasir. Servis takvimi yoksa Calendar bos doner.
    /// Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(TripRoutes.Detail)]
    [Authorize(GtfsManagementPermissions.Trips.View)]
    public virtual async Task<Result<TripDetailDto>> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetDetailAsync(id, cancellationToken);
}
