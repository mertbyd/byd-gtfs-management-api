using Byd.GtfsManagement.Dtos.Stops;

namespace Byd.GtfsManagement.Services.Stops;

// islevi: stops.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IStopAppService
    : IGtfsCrudAppService<CreateStopDto, UpdateStopDto, StopDto, StopFilter>
{
}
