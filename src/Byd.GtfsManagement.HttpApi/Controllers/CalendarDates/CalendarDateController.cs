using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.CalendarDates;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.CalendarDates;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Controllers.CalendarDates;

// islevi: Takvim istisnalari islemlerinin HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: CalendarDate use case'lerini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(CalendarDateRoutes.Root)]
public class CalendarDateController : GtfsManagementController
{
    private ICalendarDateAppService AppService => LazyGetRequiredService<ICalendarDateAppService>();

    /// <summary>Yeni takvim istisnasi kaydi ekler.</summary>
    /// <param name="input">Kaydin bagli oldugu ust kayit ve GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kayit kimligini, Feed kimligini, bagli kayitlarin kimlik ve okunabilir karsiliklarini,
    /// GTFS alanlarini ve audit zamanlarini tasir. Ust kayit istegin tenant kapsaminda degilse
    /// kayit olusturulmaz.
    /// </remarks>
    [HttpPost]
    [Authorize(GtfsManagementPermissions.CalendarDates.Manage)]
    public virtual async Task<Result<CalendarDateDto>> CreateAsync(
        [FromBody] CreateCalendarDateDto input, CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>Kimligi verilen takvim istisnasi kaydini istegin kapsaminda getirir.</summary>
    /// <param name="id">Okunacak kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kayit kimligini, Feed kimligini, bagli kayitlarin kimlik ve okunabilir karsiliklarini,
    /// GTFS alanlarini ve audit zamanlarini tasir. Bagli alt kayitlar bu endpointte okunmaz.
    /// </remarks>
    [HttpGet(CalendarDateRoutes.ById)]
    [Authorize(GtfsManagementPermissions.CalendarDates.View)]
    public virtual async Task<Result<CalendarDateDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>Filtre, siralama ve sayfalama uygulanmis takvim istisnasi kayitlarini getirir.</summary>
    /// <param name="input">Filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request { Page (int) → Sayfa numarasi, varsayilan 1; PageSize (int) → Sayfa boyutu, varsayilan 10, en fazla
    /// 1000; Sorting (DataTableSorting[]?) → Siralama alanlari; Filters (CalendarDateFilter?) → Alan bazli filtreler,
    /// her alan { Operator, Value, SecondValue, Values } tasir }
    /// Response { TotalCount (long) → Filtreye uyan toplam kayit; Items (CalendarDateDto[]) → Sayfadaki kayitlar }
    /// Sayfa yalnizca istegin tenant'indaki kayitlari tasir. Eslesen kayit yoksa toplam sayi sifir
    /// ve liste bos doner. Bu endpoint kayit yazmaz.
    /// </remarks>
    [HttpGet(CalendarDateRoutes.FilteredList)]
    [Authorize(GtfsManagementPermissions.CalendarDates.View)]
    public virtual async Task<Result<PagedResultDto<CalendarDateDto>>> GetFilteredListAsync(
        [FromQuery] DataTableListInput<CalendarDateFilter> input, CancellationToken cancellationToken = default)
        => await AppService.GetFilteredListAsync(input, cancellationToken);

    /// <summary>Kimligi verilen takvim istisnasi kaydinin GTFS alanlarini gunceller.</summary>
    /// <param name="id">Guncellenecek kaydin kimligi.</param>
    /// <param name="input">Kaydin yeni GTFS alanlari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value guncel alanlari tasir. Kaydin bagli oldugu ust kayit degistirilemez.
    /// </remarks>
    [HttpPut(CalendarDateRoutes.ById)]
    [Authorize(GtfsManagementPermissions.CalendarDates.Manage)]
    public virtual async Task<Result<CalendarDateDto>> UpdateAsync(
        Guid id, [FromBody] UpdateCalendarDateDto input, CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>Kimligi verilen takvim istisnasi kaydini siler.</summary>
    /// <param name="id">Silinecek kaydin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Govde dondurmez. Silme kalicidir; kayda bagli GTFS satirlari varsa iliski kurali silmeyi engeller.
    /// </remarks>
    [HttpDelete(CalendarDateRoutes.ById)]
    [Authorize(GtfsManagementPermissions.CalendarDates.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }
}
