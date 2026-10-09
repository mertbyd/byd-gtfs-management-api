using Byd.GtfsManagement.Dtos.Levels;

namespace Byd.GtfsManagement.Services.Levels;

// islevi: Level CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS kat CRUD API sozlesmesini bildirir.</summary>
public interface ILevelAppService
    : IGtfsCrudAppService<CreateLevelDto, UpdateLevelDto, LevelDto, LevelFilter>
{
}
