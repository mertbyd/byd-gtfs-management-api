using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareLegRules;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.FareLegRules;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.FareLegRules;

// islevi: GTFS ucret etap kurali HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: FareLegRule isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ucret etap kurali HTTP uclarini sunar.</summary>
[ApiController]
[Route(FareLegRuleRoutes.Root)]
public class FareLegRuleController : GtfsManagementController
{
    private IFareLegRuleAppService AppService => LazyGetRequiredService<IFareLegRuleAppService>();

    /// <summary>Bir Feed'e yeni bir ucret etap kurali ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, LegGroupId, NetworkId, FromAreaId, ToAreaId,
    /// FromTimeframeGroupId, ToTimeframeGroupId, GtfsFareProductId, RulePriority ve audit zamanlari alanlarini
    /// tasir. Feed istegin tenant kapsaminda degilse kayit olusturulmaz. Bu uc baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.FareLegRules.Manage)]
    public virtual async Task<Result<FareLegRuleDto>> CreateAsync(
        [FromBody] CreateFareLegRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ucret etap kurali kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, LegGroupId, NetworkId, FromAreaId, ToAreaId, FromTimeframeGroupId,
    /// ToTimeframeGroupId, GtfsFareProductId, RulePriority ve audit zamanlari alanlarini tasir. Kayit istegin
    /// tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(FareLegRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareLegRules.View)]
    public virtual async Task<Result<FareLegRuleDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis ucret etap kurali kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FareLegRuleFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId               (DataTableFilterItem?) → FeedId
    ///     LegGroupId           (DataTableFilterItem?) → LegGroupId
    ///     NetworkId            (DataTableFilterItem?) → NetworkId
    ///     FromAreaId           (DataTableFilterItem?) → FromAreaId
    ///     ToAreaId             (DataTableFilterItem?) → ToAreaId
    ///     FromTimeframeGroupId (DataTableFilterItem?) → FromTimeframeGroupId
    ///     ToTimeframeGroupId   (DataTableFilterItem?) → ToTimeframeGroupId
    ///     GtfsFareProductId    (DataTableFilterItem?) → GtfsFareProductId
    ///     RulePriority         (DataTableFilterItem?) → RulePriority
    ///     CreationTime         (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FareLegRuleDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FareLegRuleRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.FareLegRules.View)]
    public virtual async Task<Result<PagedResultDto<FareLegRuleDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FareLegRuleFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Ucret etap kurali kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, LegGroupId, NetworkId, FromAreaId, ToAreaId, FromTimeframeGroupId,
    /// ToTimeframeGroupId, GtfsFareProductId, RulePriority ve audit zamanlari alanlarini tasir. Kaydin Feed'i
    /// degistirilemez.
    /// </remarks>
    [HttpPut(FareLegRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareLegRules.Manage)]
    public virtual async Task<Result<FareLegRuleDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFareLegRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ucret etap kurali kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(FareLegRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareLegRules.Manage)]
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
    [Authorize(GtfsManagementPermissions.FareLegRules.Manage)]
    public virtual async Task<Result<FareLegRuleDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.FareLegRules.Manage)]
    public virtual async Task<Result<PagedResultDto<FareLegRuleDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<FareLegRuleFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
