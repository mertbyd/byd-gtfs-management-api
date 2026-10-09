using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareLegJoinRules;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.FareLegJoinRules;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.FareLegJoinRules;

// islevi: GTFS ucret etap birlestirme kurali HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: FareLegJoinRule isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ucret etap birlestirme kurali HTTP uclarini sunar.</summary>
[ApiController]
[Route(FareLegJoinRuleRoutes.Root)]
public class FareLegJoinRuleController : GtfsManagementController
{
    private IFareLegJoinRuleAppService AppService => LazyGetRequiredService<IFareLegJoinRuleAppService>();

    /// <summary>Bir Feed'e yeni bir ucret etap birlestirme kurali ekler.</summary>
    /// <param name="input">Kaydin yazilacagi Feed ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, FromNetworkId, ToNetworkId, FromStopId, ToStopId ve audit
    /// zamanlari alanlarini tasir. Feed istegin tenant kapsaminda degilse kayit olusturulmaz. Bu uc baska bir GTFS
    /// kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.FareLegJoinRules.Manage)]
    public virtual async Task<Result<FareLegJoinRuleDto>> CreateAsync(
        [FromBody] CreateFareLegJoinRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ucret etap birlestirme kurali kaydini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, FromNetworkId, ToNetworkId, FromStopId, ToStopId ve audit zamanlari
    /// alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(FareLegJoinRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareLegJoinRules.View)]
    public virtual async Task<Result<FareLegJoinRuleDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis ucret etap birlestirme kurali kayitlarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FareLegJoinRuleFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId        (DataTableFilterItem?) → FeedId
    ///     FromNetworkId (DataTableFilterItem?) → FromNetworkId
    ///     ToNetworkId   (DataTableFilterItem?) → ToNetworkId
    ///     FromStopId    (DataTableFilterItem?) → FromStopId
    ///     ToStopId      (DataTableFilterItem?) → ToStopId
    ///     CreationTime  (DataTableFilterItem?) → CreationTime
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FareLegJoinRuleDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FareLegJoinRuleRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.FareLegJoinRules.View)]
    public virtual async Task<Result<PagedResultDto<FareLegJoinRuleDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FareLegJoinRuleFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Ucret etap birlestirme kurali kaydinin alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin sistem kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, FromNetworkId, ToNetworkId, FromStopId, ToStopId ve audit zamanlari
    /// alanlarini tasir. Kaydin Feed'i degistirilemez.
    /// </remarks>
    [HttpPut(FareLegJoinRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareLegJoinRules.Manage)]
    public virtual async Task<Result<FareLegJoinRuleDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFareLegJoinRuleDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ucret etap birlestirme kurali kaydini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Response govdesizdir; baska GTFS kayitlarinca kullanilan kayit silinmez.</remarks>
    [HttpDelete(FareLegJoinRuleRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareLegJoinRules.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }
}
