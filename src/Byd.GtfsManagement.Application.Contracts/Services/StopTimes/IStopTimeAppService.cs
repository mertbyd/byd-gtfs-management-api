using Byd.GtfsManagement.Dtos.StopTimes;

namespace Byd.GtfsManagement.Services.StopTimes;

// islevi: StopTime CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
/// <summary>StopTime CRUD API sozlesmesini bildirir.</summary>
public interface IStopTimeAppService
    : IGtfsCrudAppService<CreateStopTimeDto, UpdateStopTimeDto, StopTimeDto, StopTimeFilter>
{
}
