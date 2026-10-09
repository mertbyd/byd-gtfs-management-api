using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Attributions;

namespace Byd.GtfsManagement.Interfaces.Attributions;

// islevi: Attribution entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Atif sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS atif repository sozlesmesini tanimlar.</summary>
public interface IAttributionRepository : IExtendedRepository<Attribution>
{
}
