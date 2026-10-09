using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Pathways;

namespace Byd.GtfsManagement.Interfaces.Pathways;

// islevi: Pathway tablosuna ozgu sorgu sozlesmesini bildirir.
// sistemdeki gorevi: Domain katmanini EF persistence uygulamasindan ayirir.
/// <summary>GTFS yuruyus yolu repository sozlesmesini tanimlar.</summary>
public interface IPathwayRepository : IExtendedRepository<Pathway>
{
}
