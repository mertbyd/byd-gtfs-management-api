using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.LocationGroupStops;

namespace Byd.GtfsManagement.Interfaces.LocationGroupStops;

// islevi: LocationGroupStop entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Konum grubu durak iliskisi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS konum grubu durak iliskisi repository sozlesmesini tanimlar.</summary>
public interface ILocationGroupStopRepository : IExtendedRepository<LocationGroupStop>
{
}
