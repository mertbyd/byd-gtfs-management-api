using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.LocationGroupStops;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.LocationGroupStops;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.LocationGroupStops;

// islevi: GTFS konum grubu durak iliskisi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: LocationGroupStop isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS konum grubu durak iliskisi HTTP uclarini sunar.</summary>
[ApiController]
[Route(LocationGroupStopRoutes.Root)]
public class LocationGroupStopController : GtfsManagementController
{
    private ILocationGroupStopAppService AppService => LazyGetRequiredService<ILocationGroupStopAppService>();

    /// <summary>Bir Feed'e yeni bir konum grubu durak iliskisi ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsLocationGroupId, GtfsStopId ve audit zamanlari alanlarini
    /// tasir. Feed istegin tenant kapsaminda degilse kayit olusturulmaz. Bu uc baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.LocationGroupStops.Manage)]
    public virtual async Task<Result<LocationGroupStopDto>> CreateAsync(
        [FromBody] CreateLocationGroupStopDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen konum grubu durak iliskisi kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsLocationGroupId, GtfsStopId ve audit zamanlari alanlarini tasir. Kayit
    /// istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(LocationGroupStopRoutes.ById)]
    [Authorize(GtfsManagementPermissions.LocationGroupStops.View)]
    public virtual async Task<Result<LocationGroupStopDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis konum grubu durak iliskisi kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (LocationGroupStopFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId              (DataTableFilterItem?) → FeedId
    ///     GtfsLocationGroupId (DataTableFilterItem?) → GtfsLocationGroupId
    ///     GtfsStopId          (DataTableFilterItem?) → GtfsStopId
    ///     CreationTime        (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (LocationGroupStopDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(LocationGroupStopRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.LocationGroupStops.View)]
    public virtual async Task<Result<PagedResultDto<LocationGroupStopDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<LocationGroupStopFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Konum grubu durak iliskisi kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsLocationGroupId, GtfsStopId ve audit zamanlari alanlarini
    /// tasir. Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(LocationGroupStopRoutes.ById)]
    [Authorize(GtfsManagementPermissions.LocationGroupStops.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.LocationGroupStopsUpdated,
        ActivityKind.Updated, nameof(LocationGroupStopDto.Id))]
    public virtual async Task<Result<LocationGroupStopDto>> UpdateAsync(
        Guid id, [FromBody] UpdateLocationGroupStopDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Konum grubu durak iliskisi kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(LocationGroupStopRoutes.ById)]
    [Authorize(GtfsManagementPermissions.LocationGroupStops.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.LocationGroupStopsDeleted,
        ActivityKind.Deleted, nameof(LocationGroupStopDto.Id))]
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
    [Authorize(GtfsManagementPermissions.LocationGroupStops.Manage)]
    public virtual async Task<Result<LocationGroupStopDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.LocationGroupStops.Manage)]
    public virtual async Task<Result<PagedResultDto<LocationGroupStopDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<LocationGroupStopFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
