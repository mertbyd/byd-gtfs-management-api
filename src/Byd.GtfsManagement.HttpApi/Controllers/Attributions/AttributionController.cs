using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Attributions;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Attributions;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Attributions;

// islevi: GTFS atif HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Attribution isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS atif HTTP uclarini sunar.</summary>
[ApiController]
[Route(AttributionRoutes.Root)]
public class AttributionController : GtfsManagementController
{
    private IAttributionAppService AppService => LazyGetRequiredService<IAttributionAppService>();

    /// <summary>Bir Feed'e yeni bir atif ekler.</summary>
    /// <param name="input">Feed, kurum, rol, guzergah, sefer ve iletisim alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, AgencyId ile AgencyName, RouteId ile RouteName, TripId ile
    /// TripName, GtfsAttributionId, OrganizationName, IsProducer, IsOperator, IsAuthority, AttributionUrl,
    /// AttributionEmail, AttributionPhone ve audit zamanlari alanlarini tasir. Ayni GTFS atif kimligi ayni Feed'de
    /// tekrar edemez.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Attributions.Manage)]
    public virtual async Task<Result<AttributionDto>> CreateAsync(
        [FromBody] CreateAttributionDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen atfi getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, AgencyId ile AgencyName, RouteId ile RouteName, TripId ile TripName,
    /// GtfsAttributionId, OrganizationName, IsProducer, IsOperator, IsAuthority, AttributionUrl, AttributionEmail,
    /// AttributionPhone ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi
    /// hatasi doner.
    /// </remarks>
    [HttpGet(AttributionRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Attributions.View)]
    public virtual async Task<Result<AttributionDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis atiflari sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (AttributionFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId            (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     AgencyId          (DataTableFilterItem?) → Bagli ajansa gore filtreler
    ///     RouteId           (DataTableFilterItem?) → Bagli guzergaha gore filtreler
    ///     TripId            (DataTableFilterItem?) → Bagli sefere gore filtreler
    ///     GtfsAttributionId (DataTableFilterItem?) → GTFS atif dis kimligine gore filtreler
    ///     OrganizationName  (DataTableFilterItem?) → Kurum adina gore filtreler
    ///     CreationTime      (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (AttributionDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(AttributionRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Attributions.View)]
    public virtual async Task<Result<PagedResultDto<AttributionDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<AttributionFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Atfin kurum, rol veya bag alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni kurum, rol, iletisim veya bag alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, AgencyId ile AgencyName, RouteId ile RouteName, TripId ile
    /// TripName, GtfsAttributionId, OrganizationName, IsProducer, IsOperator, IsAuthority, AttributionUrl,
    /// AttributionEmail, AttributionPhone ve audit zamanlari alanlarini tasir. Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(AttributionRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Attributions.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.AttributionsUpdated,
        ActivityKind.Updated, nameof(AttributionDto.Id))]
    public virtual async Task<Result<AttributionDto>> UpdateAsync(
        Guid id, [FromBody] UpdateAttributionDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Atfi kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; atif kaydi kalici olarak silinir.</remarks>
    [HttpDelete(AttributionRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Attributions.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.AttributionsDeleted,
        ActivityKind.Deleted, nameof(AttributionDto.Id))]
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
    [Authorize(GtfsManagementPermissions.Attributions.Manage)]
    public virtual async Task<Result<AttributionDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Attributions.Manage)]
    public virtual async Task<Result<PagedResultDto<AttributionDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<AttributionFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
