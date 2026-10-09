using Byd.GtfsManagement.Dtos.StopAreas;

namespace Byd.GtfsManagement.Services.StopAreas;

// islevi: StopArea CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS durak bolge iliskisi CRUD API sozlesmesini bildirir.</summary>
public interface IStopAreaAppService
    : IGtfsCrudAppService<CreateStopAreaDto, UpdateStopAreaDto, StopAreaDto, StopAreaFilter>
{
}
