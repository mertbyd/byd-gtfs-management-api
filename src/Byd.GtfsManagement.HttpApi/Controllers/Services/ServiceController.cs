using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Services;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Services;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.Services;

// islevi: Servisler islemlerinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Service use case'lerini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(ServiceRoutes.Root)]
public class ServiceController : GtfsManagementController
{
    private IServiceAppService AppService => LazyGetRequiredService<IServiceAppService>();

    /// <summary>Yeni servis kaydi ekler.</summary>
    /// <param name="input">Kaydin bagli oldugu ust kayit ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kayit kimligini, Feed kimligini, bagli kayitlarin kimlik ve okunabilir karsiliklarini,
    /// GTFS alanlarini ve audit zamanlarini tasir. Ust kayit istegin tenant kapsaminda degilse
    /// kayit olusturulmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.Services.Manage)]
    public virtual async Task<Result<ServiceDto>> CreateAsync(
        [FromBody] CreateServiceDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen servis kaydini istegin kapsaminda getirir.</summary>
    /// <param name="id">Okunacak kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kayit kimligini, Feed kimligini, bagli kayitlarin kimlik ve okunabilir karsiliklarini,
    /// GTFS alanlarini ve audit zamanlarini tasir. Bagli alt kayitlar bu endpointte okunmaz.
    /// </remarks>
    [HttpGet(ServiceRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Services.View)]
    public virtual async Task<Result<ServiceDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtre, siralama ve sayfalama uygulanmis servis kayitlarini getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request { Page (int) → Sayfa numarasi, varsayilan 1; PageSize (int) → Sayfa boyutu, varsayilan 10, en fazla
    /// 1000; Sorting (DataTableSorting[]?) → Siralama alanlari; Filters (ServiceFilter?) → Alan bazli filtreler,
    /// her alan { Operator, Value, SecondValue, Values } tasir }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (ServiceDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(ServiceRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.Services.View)]
    public virtual async Task<Result<PagedResultDto<ServiceDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<ServiceFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Kimligi verilen servis kaydinin GTFS alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel alanlari tasir. Kaydin bagli oldugu ust kayit degistirilemez.
    /// </remarks>
    [HttpPut(ServiceRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Services.Manage)]
    public virtual async Task<Result<ServiceDto>> UpdateAsync(
        Guid id, [FromBody] UpdateServiceDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Kimligi verilen servis kaydini siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Govde dondurmez. Silme kalicidir; kayda bagli GTFS satirlari varsa iliski kurali silmeyi engeller.
    /// </remarks>
    [HttpDelete(ServiceRoutes.ById)]
    [Authorize(GtfsManagementPermissions.Services.Manage)]
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
    [Authorize(GtfsManagementPermissions.Services.Manage)]
    public virtual async Task<Result<ServiceDto>> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetForFeedAsync(feedId, id, cancellationToken);

    /// <summary>Belirli surumun taslak veya gecmis kayitlarini filtreleyip sayfalar.</summary>
    /// <param name="feedId">Kapsamda listelenecek surum kimligi.</param>
    /// <param name="input">Kayit filtreleri, siralama ve sayfalama.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>Cevap toplam sayiyi ve hedef surumun sayfasini tasir; farkli surum veya kapsam acilmaz.</remarks>
    [HttpGet(GtfsRecordRoutes.ListForFeed)]
    [Authorize(GtfsManagementPermissions.Services.Manage)]
    public virtual async Task<Result<PagedResultDto<ServiceDto>>> GetFilteredListForFeedAsync(
        Guid feedId, [FromQuery] DataTableListInput<ServiceFilter> input,
        CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListForFeedAsync(feedId, input, cancellationToken);
}
