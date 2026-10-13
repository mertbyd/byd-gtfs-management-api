using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Shapes;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Shapes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Shapes;

// islevi: GTFS sekil noktasi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Shape isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS sekil noktasi HTTP uclarini sunar.</summary>
[ApiController]
[Route(ShapeRoutes.Root)]
public class ShapeController : GtfsManagementController
{
    private IShapeAppService AppService => LazyGetRequiredService<IShapeAppService>();

    /// <summary>Bir sekle yeni bir koordinat noktasi ekler.</summary>
    /// <param name="input">Feed, sekil dis kimligi, koordinat, sira ve mesafe alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsShapeId, Latitude, Longitude, Sequence, DistTraveled ve
    /// audit zamanlari alanlarini tasir. Ayni Feed ve shape_id altinda ayni sira numarasi tekrar edemez. Bu uc
    /// sefer olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Shapes.Manage)]
    public virtual async Task<Result<ShapeDto>> CreateAsync(
        [FromBody] CreateShapeDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen sekil noktasini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsShapeId, Latitude, Longitude, Sequence, DistTraveled ve audit
    /// zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(ShapeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Shapes.View)]
    public virtual async Task<Result<ShapeDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis sekil noktalarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (ShapeFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId       (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     GtfsShapeId  (DataTableFilterItem?) → Sekil dis kimligine gore filtreler
    ///     Sequence     (DataTableFilterItem?) → Sira numarasina gore filtreler
    ///     Latitude     (DataTableFilterItem?) → Enlem koordinatina gore filtreler
    ///     Longitude    (DataTableFilterItem?) → Boylam koordinatina gore filtreler
    ///     DistTraveled (DataTableFilterItem?) → Kat edilen mesafeye gore filtreler
    ///     CreationTime (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (ShapeDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(ShapeRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Shapes.View)]
    public virtual async Task<Result<PagedResultDto<ShapeDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<ShapeFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Sekil noktasinin koordinat, sira veya mesafesini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni koordinat, sira veya mesafe alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsShapeId, Latitude, Longitude, Sequence, DistTraveled ve audit
    /// zamanlari alanlarini tasir. Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(ShapeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Shapes.Manage)]
    public virtual async Task<Result<ShapeDto>> UpdateAsync(
        Guid id, [FromBody] UpdateShapeDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Sekil noktasini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; sekil noktasi kalici olarak silinir.</remarks>
    [HttpDelete(ShapeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Shapes.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }
}
