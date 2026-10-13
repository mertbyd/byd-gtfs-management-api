using Byd.GtfsManagement.Dtos.FareMedias;

namespace Byd.GtfsManagement.Services.FareMedias;

// islevi: FareMedia CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ucret medyasi CRUD API sozlesmesini bildirir.</summary>
public interface IFareMediaAppService
    : IGtfsCrudAppService<CreateFareMediaDto, UpdateFareMediaDto, FareMediaDto, FareMediaFilter>
{
}
