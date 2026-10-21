using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Frequencies;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Frequencies;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Frequencies;

// islevi: Sefer sikligi HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Frequency isteklerini ince Controller uzerinden AppService'e iletir.
/// <summary>Sefer sikligi HTTP uclarini sunar.</summary>
[ApiController]
[Route(FrequencyRoutes.Root)]
public class FrequencyController : GtfsManagementController
{
    private IFrequencyAppService AppService => LazyGetRequiredService<IFrequencyAppService>();

    /// <summary>Bir sefere siklik araligi ekler.</summary>
    /// <param name="input">Sefer, saat araligi ve headway alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value olusturulan kaydin Id, FeedId, TripId ile TripName, StartTime, EndTime, HeadwaySecs,
    /// ExactTimes ve audit zamanlari alanlarini tasir. Aralik ayni seferdeki baska bir aralikla cakismamalidir. Bu
    /// uc sefer takvimini olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Frequencies.Manage)]
    public virtual async Task<Result<FrequencyDto>> CreateAsync(
        [FromBody] CreateFrequencyDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen siklik araligini getirir.</summary>
    /// <param name="id">Okunacak kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kaydin Id, FeedId, TripId ile TripName, StartTime, EndTime, HeadwaySecs, ExactTimes ve audit
    /// zamanlari alanlarini tasir. Kayit istegin tenant kapsaminda degilse bulunamadi hatasi doner.
    /// </remarks>
    [HttpGet(FrequencyRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Frequencies.View)]
    public virtual async Task<Result<FrequencyDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtrelenmis siklik araliklarini sayfali getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (FrequencyFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId       (DataTableFilterItem?) → GTFS Feed'e gore filtreler
    ///     TripId       (DataTableFilterItem?) → Sefere gore filtreler
    ///     StartTime    (DataTableFilterItem?) → Ilk kalkis saatine gore filtreler
    ///     EndTime      (DataTableFilterItem?) → Bitis saatine gore filtreler
    ///     HeadwaySecs  (DataTableFilterItem?) → Kalkis sikligina gore filtreler
    ///     CreationTime (DataTableFilterItem?) → Olusturulma anina gore filtreler
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (FrequencyDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(FrequencyRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Frequencies.View)]
    public virtual async Task<Result<PagedResultDto<FrequencyDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<FrequencyFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Siklik araliginin duzenlenebilir alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Yeni saat araligi ve headway alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel kaydin Id, FeedId, TripId ile TripName, StartTime, EndTime, HeadwaySecs, ExactTimes ve
    /// audit zamanlari alanlarini tasir. Bagli sefer degistirilemez.
    /// </remarks>
    [HttpPut(FrequencyRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Frequencies.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.FrequenciesUpdated,
        ActivityKind.Updated, nameof(FrequencyDto.Id))]
    public virtual async Task<Result<FrequencyDto>> UpdateAsync(
        Guid id, [FromBody] UpdateFrequencyDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Siklik araligini siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Govde dondurmez; siklik kaydi kalici olarak silinir.</remarks>
    [HttpDelete(FrequencyRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Frequencies.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.FrequenciesDeleted,
        ActivityKind.Deleted, nameof(FrequencyDto.Id))]
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
    [Authorize(GtfsManagementPermissions.Frequencies.Manage)]
    public virtual async Task<Result<FrequencyDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Frequencies.Manage)]
    public virtual async Task<Result<PagedResultDto<FrequencyDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<FrequencyFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
