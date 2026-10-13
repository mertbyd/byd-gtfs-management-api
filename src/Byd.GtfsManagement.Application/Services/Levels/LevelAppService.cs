using Byd.GtfsManagement.Dtos.Levels;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Interfaces.Levels;
using Byd.GtfsManagement.Managers.Levels;
using Byd.GtfsManagement.Mappers.Levels;
using Byd.GtfsManagement.Models.Levels;

namespace Byd.GtfsManagement.Services.Levels;

// islevi: Level CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS kat CRUD servisini ortak tabana baglar.</summary>
public class LevelAppService
    : GtfsCrudAppServiceBase<
        Level, CreateLevelDto, UpdateLevelDto, LevelDto, LevelFilter,
        LevelCreateModel, LevelUpdateModel, LevelManager, ILevelRepository, LevelMapper>,
        ILevelAppService
{
}
