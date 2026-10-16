using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Transfers;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Transfers;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Transfers;

// islevi: GTFS aktarma HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Transfer isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS aktarma HTTP uclarini sunar.</summary>
[ApiController]
[Route(TransferRoutes.Root)]
public class TransferController : GtfsManagementController
{
    private ITransferAppService AppService => LazyGetRequiredService<ITransferAppService>();

    /// <summary>Organizasyonun aktif Feed'ine yeni bir aktarma kurali ekler.</summary>
    /// <param name="input">Aktarma baglari ve davranis kodu.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, FromStopId ile FromStopName, ToStopId ile ToStopName,
    /// FromRouteId ile FromRouteName, ToRouteId ile ToRouteName, FromTripId ile FromTripName, ToTripId ile
    /// ToTripName, TransferType, MinTransferTime ve audit zamanlari alanlarini tasir. Bu uc sefer olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Transfers.Manage)]
    public virtual async Task<Result<TransferDto>> CreateAsync(
        [FromBody] CreateTransferDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen aktarmayi getirir.</summary>
    /// <param name="id">Okunacak kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, FromStopId ile FromStopName, ToStopId ile ToStopName, FromRouteId ile
    /// FromRouteName, ToRouteId ile ToRouteName, FromTripId ile FromTripName, ToTripId ile ToTripName,
    /// TransferType, MinTransferTime ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse
    /// bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(TransferRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Transfers.View)]
    public virtual async Task<Result<TransferDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis aktarmalari sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (TransferFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId       (DataTableFilterItem?) → Veri seti Feed'ine gore filtreler
    ///     FromStopId   (DataTableFilterItem?) → Baslangic duragina gore filtreler
    ///     ToStopId     (DataTableFilterItem?) → Varis duragina gore filtreler
    ///     FromRouteId  (DataTableFilterItem?) → Baslangic guzergahina gore filtreler
    ///     ToRouteId    (DataTableFilterItem?) → Varis guzergahina gore filtreler
    ///     FromTripId   (DataTableFilterItem?) → Baslangic seferine gore filtreler
    ///     ToTripId     (DataTableFilterItem?) → Varis seferine gore filtreler
    ///     TransferType (DataTableFilterItem?) → Aktarma turune gore filtreler
    ///     CreationTime (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (TransferDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(TransferRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Transfers.View)]
    public virtual async Task<Result<PagedResultDto<TransferDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<TransferFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Aktarma kuralini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni baglar ve aktarma davranisi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, FromStopId ile FromStopName, ToStopId ile ToStopName, FromRouteId
    /// ile FromRouteName, ToRouteId ile ToRouteName, FromTripId ile FromTripName, ToTripId ile ToTripName,
    /// TransferType, MinTransferTime ve audit zamanlari alanlarini tasir. Feed degismez.
    /// </remarks>
    [HttpPut(TransferRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Transfers.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.TransfersUpdated,
        ActivityKind.Updated, nameof(TransferDto.Id))]
    public virtual async Task<Result<TransferDto>> UpdateAsync(
        Guid id, [FromBody] UpdateTransferDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Aktarma kuralini siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; aktarma kaydi kalici olarak silinir.</remarks>
    [HttpDelete(TransferRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Transfers.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.TransfersDeleted,
        ActivityKind.Deleted, nameof(TransferDto.Id))]
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
    [Authorize(GtfsManagementPermissions.Transfers.Manage)]
    public virtual async Task<Result<TransferDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Transfers.Manage)]
    public virtual async Task<Result<PagedResultDto<TransferDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<TransferFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
