using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Agencies;
using Byd.GtfsManagement.Localization;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Agencies;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Attributes;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Agencies;

// islevi: agency.txt islemlerinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Agency use case'lerini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(AgencyRoutes.Root)]
public class AgencyController : GtfsManagementController
{
    private IAgencyAppService AppService => LazyGetRequiredService<IAgencyAppService>();

    /// <summary>Organizasyonun aktif GTFS Feed'ine yeni ajans kaydi ekler.</summary>
    /// <param name="input">agency.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value ajans kimligini, Feed kimligini, cEMV lookup kimligi ile adini, GTFS alanlarini ve audit
    /// zamanlarini tasir. Feed istegin tenant kapsaminda degilse kayit olusturulmaz; agency_id
    /// bos birakildiginda Feed'de baska ajans bulunamaz. Bu endpoint route veya trip kaydi olusturmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Agencies.Manage)]
    public virtual async Task<Result<AgencyDto>> CreateAsync(
        [FromBody] CreateAgencyDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ajansi istegin kapsaminda getirir.</summary>
    /// <param name="id">Okunacak Agency kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value ajans kimligini, Feed kimligini, cEMV lookup kimligi ile adini, GTFS alanlarini ve audit
    /// zamanlarini tasir. Ajansin route listesi bu endpointte okunmaz.
    /// </remarks>
    [HttpGet(AgencyRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Agencies.View)]
    public virtual async Task<Result<AgencyDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtre, siralama ve sayfalama uygulanmis ajanslari getirir.</summary>
    /// <param name="input">Ajans filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Page     (int)                 → Sayfa numarasi; varsayilan 1
    ///   PageSize (int)                 → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting  (DataTableSorting[]?) → Siralama alanlari ve yonleri
    ///   Filters  (AgencyFilter?)       → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     FeedId  (DataTableFilterItem?) → Feed filtresi
    ///     CemvSupportId  (DataTableFilterItem?) → cEMV lookup filtresi
    ///     GtfsAgencyId   (DataTableFilterItem?) → GTFS agency_id filtresi
    ///     Name           (DataTableFilterItem?) → Ajans adi filtresi
    ///     Timezone       (DataTableFilterItem?) → Saat dilimi filtresi
    ///     Language       (DataTableFilterItem?) → Dil kodu filtresi
    ///     CreationTime   (DataTableFilterItem?) → Kayit zamani filtresi
    /// }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (AgencyDto[]) → Sayfadaki ajanslar }
    /// Sayfa yalnizca istegin tenant'indaki ajanslari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(AgencyRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Agencies.View)]
    public virtual async Task<Result<PagedResultDto<AgencyDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<AgencyFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Kimligi verilen ajansin GTFS alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek Agency kaydinin kimligi.</param>
    /// <param name="input">Ajansin yeni agency.txt alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel ajans alanlarini tasir. Yalniz organizasyonun aktif Feed'indeki kayit guncellenir;
    /// kaydin bagli oldugu Feed degistirilemez.
    /// </remarks>
    [HttpPut(AgencyRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Agencies.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.AgenciesUpdated,
        ActivityKind.Updated, nameof(AgencyDto.Id))]
    public virtual async Task<Result<AgencyDto>> UpdateAsync(
        Guid id, [FromBody] UpdateAgencyDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Kimligi verilen ajansi siler.</summary>
    /// <param name="id">Silinecek Agency kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Govde dondurmez. Silme kalicidir; ajansa bagli GTFS kayitlari varsa iliski kurali silmeyi engeller.
    /// </remarks>
    [HttpDelete(AgencyRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Agencies.Manage)]
    [TrackActivity<GtfsManagementResource>(
        GtfsManagementLocalizationKeys.ActivityLogs.AgenciesDeleted,
        ActivityKind.Deleted, nameof(AgencyDto.Id))]
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
    [Authorize(GtfsManagementPermissions.Agencies.Manage)]
    public virtual async Task<Result<AgencyDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Agencies.Manage)]
    public virtual async Task<Result<PagedResultDto<AgencyDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<AgencyFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
