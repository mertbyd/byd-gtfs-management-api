using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FeedInfos;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.FeedInfos;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.FeedInfos;

// islevi: GTFS yayimci bilgisi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: FeedInfo isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>GTFS yayimci bilgisi HTTP uclarini sunar.</summary>
[ApiController]
[Route(FeedInfoRoutes.Root)]
public class FeedInfoController : GtfsManagementController
{
    private IFeedInfoAppService AppService => LazyGetRequiredService<IFeedInfoAppService>();

    /// <summary>Organizasyonun aktif Feed'ine yayimci bilgisini olusturur.</summary>
    /// <param name="input">Yayimci adi, web adresi, dil ve tarih alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, PublisherName, PublisherUrl, Language, DefaultLanguage,
    /// StartDate, EndDate, Version, ContactEmail, ContactUrl ve audit zamanlari alanlarini tasir. Bir Feed icinde
    /// yalnizca bir yayimci kaydi bulunabilir. Bu uc servis tanimlamaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.FeedInfos.Manage)]
    public virtual async Task<Result<FeedInfoDto>> CreateAsync(
        [FromBody] CreateFeedInfoDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen yayimci bilgisini getirir.</summary>
    /// <param name="id">Okunacak kaydin sistem kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, PublisherName, PublisherUrl, Language, DefaultLanguage, StartDate,
    /// EndDate, Version, ContactEmail, ContactUrl ve audit zamanlari alanlarini tasir. Kayit istegin tenant
    /// kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(FeedInfoRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FeedInfos.View)]
    public virtual async Task<Result<FeedInfoDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis yayimci bilgilerini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FeedInfoFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId          (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     PublisherName   (DataTableFilterItem?) → Yayimci adina gore filtreler
    ///     Language        (DataTableFilterItem?) → Dile gore filtreler
    ///     DefaultLanguage (DataTableFilterItem?) → Varsayilan dile gore filtreler
    ///     StartDate       (DataTableFilterItem?) → Baslangic tarihine gore filtreler
    ///     EndDate         (DataTableFilterItem?) → Bitis tarihine gore filtreler
    ///     Version         (DataTableFilterItem?) → Feede gore filtreler
    ///     CreationTime    (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FeedInfoDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FeedInfoRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.FeedInfos.View)]
    public virtual async Task<Result<PagedResultDto<FeedInfoDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FeedInfoFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Yayimci bilgisinin duzenlenebilir alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni yayimci adi, url, dil veya iletisim alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, PublisherName, PublisherUrl, Language, DefaultLanguage, StartDate,
    /// EndDate, Version, ContactEmail, ContactUrl ve audit zamanlari alanlarini tasir. Bagli Feed degistirilemez.
    /// </remarks>
    [HttpPut(FeedInfoRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FeedInfos.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.FeedInfosUpdated,
        ActivityKind.Updated, nameof(FeedInfoDto.Id))]
    public virtual async Task<Result<FeedInfoDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFeedInfoDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Yayimci bilgisini kalici olarak siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; yayimci bilgisi kalici olarak silinir.</remarks>
    [HttpDelete(FeedInfoRoutes.ById)]
    [Authorize(GtfsManagementPermissions.FeedInfos.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.FeedInfosDeleted,
        ActivityKind.Deleted, nameof(FeedInfoDto.Id))]
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
    [Authorize(GtfsManagementPermissions.FeedInfos.Manage)]
    public virtual async Task<Result<FeedInfoDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.FeedInfos.Manage)]
    public virtual async Task<Result<PagedResultDto<FeedInfoDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<FeedInfoFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
