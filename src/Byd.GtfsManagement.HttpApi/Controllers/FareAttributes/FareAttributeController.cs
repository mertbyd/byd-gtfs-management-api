using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareAttributes;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.FareAttributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.FareAttributes;

// islevi: GTFS ucret ozelligi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: FareAttribute isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS ucret ozelligi HTTP uclarini sunar.</summary>
[ApiController]
[Route(FareAttributeRoutes.Root)]
public class FareAttributeController : GtfsManagementController
{
    private IFareAttributeAppService AppService => LazyGetRequiredService<IFareAttributeAppService>();

    /// <summary>Bir Feed'e yeni bir ucret sinifi tanimi ekler.</summary>
    /// <param name="input">Feed, ucret kimligi, fiyat, para birimi ve transfer kurallari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, AgencyId ile AgencyName, GtfsFareId, Price, CurrencyType,
    /// PaymentMethod, Transfers, TransferDuration ve audit zamanlari alanlarini tasir. Ayni Feed icinde fare_id
    /// tekrar edemez. Bu uc ajans olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.FareAttributes.Manage)]
    public virtual async Task<Result<FareAttributeDto>> CreateAsync(
        [FromBody] CreateFareAttributeDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ucret sinifini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, AgencyId ile AgencyName, GtfsFareId, Price, CurrencyType, PaymentMethod,
    /// Transfers, TransferDuration ve audit zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse
    /// bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(FareAttributeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareAttributes.View)]
    public virtual async Task<Result<FareAttributeDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis ucret siniflarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FareAttributeFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId           (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     GtfsFareId       (DataTableFilterItem?) → Ucret dis kimligine gore filtreler
    ///     AgencyId         (DataTableFilterItem?) → Ajansa gore filtreler
    ///     Price            (DataTableFilterItem?) → Fiyata gore filtreler
    ///     CurrencyType     (DataTableFilterItem?) → Para birimine gore filtreler
    ///     PaymentMethod    (DataTableFilterItem?) → Odeme yontemine gore filtreler
    ///     Transfers        (DataTableFilterItem?) → Aktarma sayisina gore filtreler
    ///     TransferDuration (DataTableFilterItem?) → Aktarma suresine gore filtreler
    ///     CreationTime     (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FareAttributeDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FareAttributeRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.FareAttributes.View)]
    public virtual async Task<Result<PagedResultDto<FareAttributeDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FareAttributeFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Ucret sinifinin fiyat, para birimi veya transfer kurallarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni fiyat, para birimi veya aktarma alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, AgencyId ile AgencyName, GtfsFareId, Price, CurrencyType,
    /// PaymentMethod, Transfers, TransferDuration ve audit zamanlari alanlarini tasir. Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(FareAttributeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareAttributes.Manage)]
    public virtual async Task<Result<FareAttributeDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFareAttributeDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Ucret sinifini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; ucret kaydi kalici olarak silinir.</remarks>
    [HttpDelete(FareAttributeRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FareAttributes.Manage)]
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
    [Authorize(GtfsManagementPermissions.FareAttributes.Manage)]
    public virtual async Task<Result<FareAttributeDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.FareAttributes.Manage)]
    public virtual async Task<Result<PagedResultDto<FareAttributeDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<FareAttributeFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
