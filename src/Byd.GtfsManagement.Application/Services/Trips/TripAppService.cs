using Byd.GtfsManagement.Dtos.Trips;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces.Trips;
using Byd.GtfsManagement.Managers.Trips;
using Byd.GtfsManagement.Mappers.Trips;
using Byd.GtfsManagement.Models.Trips;

namespace Byd.GtfsManagement.Services.Trips;

// islevi: Trip kayitlarinin CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class TripAppService
    : GtfsCrudAppServiceBase<
        Trip, CreateTripDto, UpdateTripDto, TripDto, TripFilter,
        TripCreateModel, TripUpdateModel, TripManager, ITripRepository, TripMapper>, ITripAppService
{
}
