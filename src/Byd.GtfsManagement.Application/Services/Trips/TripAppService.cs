using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Dtos.Trips;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces.Trips;
using Byd.GtfsManagement.Managers.Trips;
using Byd.GtfsManagement.Mappers.Trips;
using Byd.GtfsManagement.Models.Trips;

namespace Byd.GtfsManagement.Services.Trips;

// islevi: Trip kayitlarinin CRUD akisini ortak tabana baglar; kaydi duraklari, sikliklari, sekli ve takvimiyle okuyan detay ucunu sunar.
// sistemdeki gorevi: Tipli baglari bildirir; CRUD tabanda kalir, detay kurali TripManager'dadir.
public class TripAppService
    : GtfsCrudAppServiceBase<
        Trip, CreateTripDto, UpdateTripDto, TripDto, TripFilter,
        TripCreateModel, TripUpdateModel, TripManager, ITripRepository, TripMapper>, ITripAppService
{
    // Kaydi duraklari, sikliklari, sekli ve takvimiyle birlikte getirir.
    public virtual async Task<TripDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.GetDetailAsync(id, cancellationToken));
}
