using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Levels;

namespace Byd.GtfsManagement.Interfaces.Levels;

// islevi: Level tablosuna ozgu sorgu sozlesmesini bildirir.
// sistemdeki gorevi: Domain katmanini EF persistence uygulamasindan ayirir.
/// <summary>GTFS kat repository sozlesmesini tanimlar.</summary>
public interface ILevelRepository : IExtendedRepository<Level>
{
}
