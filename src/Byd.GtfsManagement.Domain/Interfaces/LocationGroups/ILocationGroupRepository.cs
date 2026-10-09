using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.LocationGroups;

namespace Byd.GtfsManagement.Interfaces.LocationGroups;

// islevi: LocationGroup entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Konum grubu sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS konum grubu repository sozlesmesini tanimlar.</summary>
public interface ILocationGroupRepository : IExtendedRepository<LocationGroup>
{
}
