using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareTransferRules;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.FareTransferRules;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.FareTransferRules;

// islevi: GTFS ucret aktarma kurali HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: FareTransferRule isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ucret aktarma kurali HTTP uclarini sunar.</summary>
[ApiController]
[Route(FareTransferRuleRoutes.Root)]
public class FareTransferRuleController : GtfsManagementController
{
    private IFareTransferRuleAppService AppService => LazyGetRequiredService<IFareTransferRuleAppService>();

    /// <summary>Bir Feed'e yeni bir ucret aktarma kurali ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, FromLegGroupId, ToLegGroupId, GtfsFareProductId,
    /// TransferCount, DurationLimit, DurationLimitType, FareTransferType ve audit zamanlari alanlarini tasir. Feed
    /// istegin tenant kapsaminda degilse kayit olusturulmaz. Bu uc baska bir GTFS kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.FareTransferRules.Manage)]
    public virtual async Task<Result<FareTransferRuleDto>> CreateAsync(
        [FromBody] CreateFareTransferRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ucret aktarma kurali kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, FromLegGroupId, ToLegGroupId, GtfsFareProductId, TransferCount,
    /// DurationLimit, DurationLimitType, FareTransferType ve audit zamanlari alanlarini tasir. Kayit istegin tenant
    /// kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(FareTransferRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareTransferRules.View)]
    public virtual async Task<Result<FareTransferRuleDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis ucret aktarma kurali kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FareTransferRuleFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId            (DataTableFilterItem?) → FeedId
    ///     FromLegGroupId    (DataTableFilterItem?) → FromLegGroupId
    ///     ToLegGroupId      (DataTableFilterItem?) → ToLegGroupId
    ///     TransferCount     (DataTableFilterItem?) → TransferCount
    ///     DurationLimit     (DataTableFilterItem?) → DurationLimit
    ///     DurationLimitType (DataTableFilterItem?) → DurationLimitType
    ///     FareTransferType  (DataTableFilterItem?) → FareTransferType
    ///     GtfsFareProductId (DataTableFilterItem?) → GtfsFareProductId
    ///     CreationTime      (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FareTransferRuleDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FareTransferRuleRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.FareTransferRules.View)]
    public virtual async Task<Result<PagedResultDto<FareTransferRuleDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FareTransferRuleFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Ucret aktarma kurali kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, FromLegGroupId, ToLegGroupId, GtfsFareProductId, TransferCount,
    /// DurationLimit, DurationLimitType, FareTransferType ve audit zamanlari alanlarini tasir. Kaydin Feed'i
    /// degistirilemez.
    /// </remarks>
    [HttpPut(FareTransferRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareTransferRules.Manage)]
    public virtual async Task<Result<FareTransferRuleDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFareTransferRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ucret aktarma kurali kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(FareTransferRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareTransferRules.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }
}
