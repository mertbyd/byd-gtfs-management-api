using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareRules;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.FareRules;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.FareRules;

// islevi: GTFS ucret kurali HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: FareRule isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ucret kurali HTTP uclarini sunar.</summary>
[ApiController]
[Route(FareRuleRoutes.Root)]
public class FareRuleController : GtfsManagementController
{
    private IFareRuleAppService AppService => LazyGetRequiredService<IFareRuleAppService>();

    /// <summary>Bir Feed'e yeni bir ucret kurali eslemesi ekler.</summary>
    /// <param name="input">Feed, ucret tanimi, guzergah ve bolge alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, FareAttributeId ile FareAttributeName, RouteId ile RouteName,
    /// OriginId, DestinationId, ContainsId ve audit zamanlari alanlarini tasir. Ayni kural kombinasyonu ayni
    /// Feed'de tekrar edemez.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.FareRules.Manage)]
    public virtual async Task<Result<FareRuleDto>> CreateAsync(
        [FromBody] CreateFareRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ucret kuralini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, FareAttributeId ile FareAttributeName, RouteId ile RouteName, OriginId,
    /// DestinationId, ContainsId ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse
    /// bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(FareRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareRules.View)]
    public virtual async Task<Result<FareRuleDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis ucret kurallarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FareRuleFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId          (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     FareAttributeId (DataTableFilterItem?) → Ucret tanimina gore filtreler
    ///     RouteId         (DataTableFilterItem?) → Guzergaha gore filtreler
    ///     OriginId        (DataTableFilterItem?) → Baslangic bolgesine gore filtreler
    ///     DestinationId   (DataTableFilterItem?) → Varis bolgesine gore filtreler
    ///     ContainsId      (DataTableFilterItem?) → Gecis bolgesine gore filtreler
    ///     CreationTime    (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FareRuleDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FareRuleRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.FareRules.View)]
    public virtual async Task<Result<PagedResultDto<FareRuleDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FareRuleFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Ucret kuralinin guzergah veya bolge eslemelerini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni guzergah veya bolge alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, FareAttributeId ile FareAttributeName, RouteId ile RouteName,
    /// OriginId, DestinationId, ContainsId ve audit zamanlari alanlarini tasir. Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(FareRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareRules.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.FareRulesUpdated,
        ActivityKind.Updated, nameof(FareRuleDto.Id))]
    public virtual async Task<Result<FareRuleDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFareRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ucret kuralini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; ucret kurali kaydi kalici olarak silinir.</remarks>
    [HttpDelete(FareRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareRules.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.FareRulesDeleted,
        ActivityKind.Deleted, nameof(FareRuleDto.Id))]
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
    [Authorize(GtfsManagementPermissions.FareRules.Manage)]
    public virtual async Task<Result<FareRuleDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.FareRules.Manage)]
    public virtual async Task<Result<PagedResultDto<FareRuleDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<FareRuleFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
