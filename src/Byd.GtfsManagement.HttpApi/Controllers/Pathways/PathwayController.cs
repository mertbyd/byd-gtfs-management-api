using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Pathways;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Pathways;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Pathways;

// islevi: GTFS yuruyus yolu HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Pathway isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS yuruyus yolu HTTP uclarini sunar.</summary>
[ApiController]
[Route(PathwayRoutes.Root)]
public class PathwayController : GtfsManagementController
{
    private IPathwayAppService AppService => LazyGetRequiredService<IPathwayAppService>();

    /// <summary>Bir Feed'e yeni bir istasyon yuruyus yolu ekler.</summary>
    /// <param name="input">Feed, yuruyus yolu dis kimligi, durak ve olcu alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, FromStopId ile FromStopName, ToStopId ile ToStopName,
    /// GtfsPathwayId, PathwayMode, IsBidirectional, Length, TraversalTime, StairCount, MaxSlope, MinWidth,
    /// SignpostedAs, ReversedSignpostedAs ve audit zamanlari alanlarini tasir. Ayni Feed icinde pathway_id tekrar
    /// edemez. Bu uc durak olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Pathways.Manage)]
    public virtual async Task<Result<PathwayDto>> CreateAsync(
        [FromBody] CreatePathwayDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen yuruyus yolunu getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, FromStopId ile FromStopName, ToStopId ile ToStopName, GtfsPathwayId,
    /// PathwayMode, IsBidirectional, Length, TraversalTime, StairCount, MaxSlope, MinWidth, SignpostedAs,
    /// ReversedSignpostedAs ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi
    /// hatasi doner.
    /// </remarks>
    [HttpGet(PathwayRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Pathways.View)]
    public virtual async Task<Result<PathwayDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis yuruyus yollarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (PathwayFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId          (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     GtfsPathwayId   (DataTableFilterItem?) → Yol dis kimligine gore filtreler
    ///     FromStopId      (DataTableFilterItem?) → Baslangic duragina gore filtreler
    ///     ToStopId        (DataTableFilterItem?) → Varis duragina gore filtreler
    ///     PathwayMode     (DataTableFilterItem?) → Yol moduna gore filtreler
    ///     IsBidirectional (DataTableFilterItem?) → Cift yon durumuna gore filtreler
    ///     Length          (DataTableFilterItem?) → Uzunluga gore filtreler
    ///     TraversalTime   (DataTableFilterItem?) → Gecis suresine gore filtreler
    ///     CreationTime    (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (PathwayDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(PathwayRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Pathways.View)]
    public virtual async Task<Result<PagedResultDto<PathwayDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<PathwayFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Yuruyus yolunun durak, mod veya olcu bilgilerini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni yuruyus yolu dizini veya ad alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, FromStopId ile FromStopName, ToStopId ile ToStopName,
    /// GtfsPathwayId, PathwayMode, IsBidirectional, Length, TraversalTime, StairCount, MaxSlope, MinWidth,
    /// SignpostedAs, ReversedSignpostedAs ve audit zamanlari alanlarini tasir. Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(PathwayRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Pathways.Manage)]
    public virtual async Task<Result<PathwayDto>> UpdateAsync(
        Guid id, [FromBody] UpdatePathwayDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Yuruyus yolunu kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; yuruyus yolu kaydi kalici olarak silinir.</remarks>
    [HttpDelete(PathwayRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Pathways.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }
}
