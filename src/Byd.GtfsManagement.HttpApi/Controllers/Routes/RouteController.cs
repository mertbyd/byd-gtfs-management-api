using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Routes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Routes;

// islevi: routes.txt islemlerinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Route use case'lerini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(RouteRoutes.Root)]
public class RouteController : GtfsManagementController
{
    private IRouteAppService AppService => LazyGetRequiredService<IRouteAppService>();

    /// <summary>Hedef GTFS Feed'e yeni guzergah kaydi ekler.</summary>
    /// <param name="input">Guzergahin yazilacagi Feed ve routes.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guzergah kimligini, Feed kimligini, ajans, ucret agi ve lookup kimlikleri ile adlarini, GTFS
    /// alanlarini ve audit zamanlarini tasir. Kisa ad ile uzun addan en az biri zorunludur; Feed'de birden
    /// fazla ajans varsa ajans secimi zorunludur. Bu endpoint trip veya stop_times kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Routes.Manage)]
    public virtual async Task<Result<RouteDto>> CreateAsync(
        [FromBody] CreateRouteDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen guzergahi istegin kapsaminda getirir.</summary>
    /// <param name="id">Okunacak Route kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guzergah kimligini, Feed kimligini, ajans, ucret agi ve lookup kimlikleri ile adlarini, GTFS
    /// alanlarini ve audit zamanlarini tasir. Guzergahin seferleri bu endpointte okunmaz.
    /// </remarks>
    [HttpGet(RouteRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Routes.View)]
    public virtual async Task<Result<RouteDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtre, siralama ve sayfalama uygulanmis guzergahlari getirir.</summary>
    /// <param name="input">Guzergah filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (RouteFilter?)        → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId (DataTableFilterItem?) → Feed filtresi
    ///     AgencyId      (DataTableFilterItem?) → Ajans filtresi
    ///     RouteTypeId   (DataTableFilterItem?) → Ulasim turu filtresi
    ///     GtfsRouteId   (DataTableFilterItem?) → GTFS route_id filtresi
    ///     ShortName     (DataTableFilterItem?) → Kisa ad filtresi
    ///     LongName      (DataTableFilterItem?) → Uzun ad filtresi
    ///     SortOrder     (DataTableFilterItem?) → Gosterim sirasi filtresi
    ///     CreationTime  (DataTableFilterItem?) → Kayit zamani filtresi
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (RouteDto[]) → Sayfadaki guzergahlar }
    /// Sayfa yalnizca istegin tenant'indaki guzergahlari tasir. Eslesen kayit yoksa toplam sayi
    /// sifir ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(RouteRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Routes.View)]
    public virtual async Task<Result<PagedResultDto<RouteDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<RouteFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Kimligi verilen guzergahin GTFS alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek Route kaydinin kimligi.</param>
    /// <param name="input">Guzergahin yeni routes.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel guzergah alanlarini tasir. Kaydin bagli oldugu Feed degistirilemez; secilen ajans
    /// ayni Feed'de olmalidir.
    /// </remarks>
    [HttpPut(RouteRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Routes.Manage)]
    public virtual async Task<Result<RouteDto>> UpdateAsync(
        Guid id, [FromBody] UpdateRouteDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Kimligi verilen guzergahi siler.</summary>
    /// <param name="id">Silinecek Route kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Govde dondurmez. Silme kalicidir; guzergaha bagli seferler varsa iliski kurali silmeyi engeller.
    /// </remarks>
    [HttpDelete(RouteRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Routes.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }
}
