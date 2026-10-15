using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Levels;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Levels;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Levels;

// islevi: GTFS kat HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Level isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS kat HTTP uclarini sunar.</summary>
[ApiController]
[Route(LevelRoutes.Root)]
public class LevelController : GtfsManagementController
{
    private ILevelAppService AppService => LazyGetRequiredService<ILevelAppService>();

    /// <summary>Bir Feed'e yeni bir istasyon kati ekler.</summary>
    /// <param name="input">Feed, kat dis kimligi, dizin ve ad alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, GtfsLevelId, LevelIndex, Name ve audit zamanlari alanlarini
    /// tasir. Ayni Feed icinde level_id tekrar edemez. Bu uc durak olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Levels.Manage)]
    public virtual async Task<Result<LevelDto>> CreateAsync(
        [FromBody] CreateLevelDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen kati getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, GtfsLevelId, LevelIndex, Name ve audit zamanlari alanlarini tasir. Kayit
    /// istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(LevelRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Levels.View)]
    public virtual async Task<Result<LevelDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis katlari sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (LevelFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId       (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     GtfsLevelId  (DataTableFilterItem?) → Kat dis kimligine gore filtreler
    ///     LevelIndex   (DataTableFilterItem?) → Kat dizinine gore filtreler
    ///     Name         (DataTableFilterItem?) → Kat adina gore filtreler
    ///     CreationTime (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (LevelDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(LevelRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Levels.View)]
    public virtual async Task<Result<PagedResultDto<LevelDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<LevelFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Katin dizin veya ad bilgilerini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni kat dizini veya ad alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, GtfsLevelId, LevelIndex, Name ve audit zamanlari alanlarini tasir.
    /// Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(LevelRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Levels.Manage)]
    public virtual async Task<Result<LevelDto>> UpdateAsync(
        Guid id, [FromBody] UpdateLevelDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Kati kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; kat kaydi kalici olarak silinir.</remarks>
    [HttpDelete(LevelRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Levels.Manage)]
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
    [Authorize(GtfsManagementPermissions.Levels.Manage)]
    public virtual async Task<Result<LevelDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Levels.Manage)]
    public virtual async Task<Result<PagedResultDto<LevelDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<LevelFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
