using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.StopTimes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.StopTimes;

// islevi: Sefer durak zamanlarinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: StopTime use case'lerini ince Controller uzerinden AppService'e yonlendirir.
/// <summary>StopTime HTTP uclarini sunar.</summary>
[ApiController]
[Route(StopTimeRoutes.Root)]
public class StopTimeController : GtfsManagementController
{
    private IStopTimeAppService AppService => LazyGetRequiredService<IStopTimeAppService>();

    /// <summary>Bir seferin sirali durak zamani kaydini ekler.</summary>
    /// <param name="input">Sefer, durak, sira ve GTFS zaman alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, TripId ile TripName, StopId ile StopName, LocationGroupId ile
    /// LocationGroupName, PickupTypeId ile PickupTypeName, DropOffTypeId ile DropOffTypeName, ContinuousPickupId
    /// ile ContinuousPickupName, ContinuousDropOffId ile ContinuousDropOffName, PickupBookingRuleId,
    /// DropOffBookingRuleId, StopSequence, ArrivalTime, DepartureTime, StopHeadsign, StartPickupDropOffWindow,
    /// EndPickupDropOffWindow, ShapeDistTraveled, Timepoint ve audit zamanlari alanlarini tasir. Sefer ile durak
    /// ayni Feed icinde olmalidir. Bu uc tum seferi export etmez.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.StopTimes.Manage)]
    public virtual async Task<Result<StopTimeDto>> CreateAsync(
        [FromBody] CreateStopTimeDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen durak zamani kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, TripId ile TripName, StopId ile StopName, LocationGroupId ile
    /// LocationGroupName, PickupTypeId ile PickupTypeName, DropOffTypeId ile DropOffTypeName, ContinuousPickupId
    /// ile ContinuousPickupName, ContinuousDropOffId ile ContinuousDropOffName, PickupBookingRuleId,
    /// DropOffBookingRuleId, StopSequence, ArrivalTime, DepartureTime, StopHeadsign, StartPickupDropOffWindow,
    /// EndPickupDropOffWindow, ShapeDistTraveled, Timepoint ve audit zamanlari alanlarini tasir. Kayit istegin
    /// tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(StopTimeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.StopTimes.View)]
    public virtual async Task<Result<StopTimeDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis durak zamani kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (StopTimeFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId        (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     TripId        (DataTableFilterItem?) → Sefere gore filtreler
    ///     StopId        (DataTableFilterItem?) → Duraga gore filtreler
    ///     StopSequence  (DataTableFilterItem?) → Sefer icindeki durak sirasina gore filtreler
    ///     ArrivalTime   (DataTableFilterItem?) → Varis saatine gore filtreler
    ///     DepartureTime (DataTableFilterItem?) → Kalkis saatine gore filtreler
    ///     CreationTime  (DataTableFilterItem?) → Kaydin olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (StopTimeDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(StopTimeRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.StopTimes.View)]
    public virtual async Task<Result<PagedResultDto<StopTimeDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<StopTimeFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Durak zamani kaydinin duzenlenebilir alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni durak, sira ve zaman alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, TripId ile TripName, StopId ile StopName, LocationGroupId ile
    /// LocationGroupName, PickupTypeId ile PickupTypeName, DropOffTypeId ile DropOffTypeName, ContinuousPickupId
    /// ile ContinuousPickupName, ContinuousDropOffId ile ContinuousDropOffName, PickupBookingRuleId,
    /// DropOffBookingRuleId, StopSequence, ArrivalTime, DepartureTime, StopHeadsign, StartPickupDropOffWindow,
    /// EndPickupDropOffWindow, ShapeDistTraveled, Timepoint ve audit zamanlari alanlarini tasir. Kaydin bagli
    /// oldugu sefer degistirilemez.
    /// </remarks>
    [HttpPut(StopTimeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.StopTimes.Manage)]
    public virtual async Task<Result<StopTimeDto>> UpdateAsync(
        Guid id, [FromBody] UpdateStopTimeDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Durak zamani kaydini siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; kayit kalici olarak silinir.</remarks>
    [HttpDelete(StopTimeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.StopTimes.Manage)]
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
    [Authorize(GtfsManagementPermissions.StopTimes.Manage)]
    public virtual async Task<Result<StopTimeDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.StopTimes.Manage)]
    public virtual async Task<Result<PagedResultDto<StopTimeDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<StopTimeFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
