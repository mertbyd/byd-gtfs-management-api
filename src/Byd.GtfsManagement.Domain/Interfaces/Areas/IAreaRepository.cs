using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Areas;

namespace Byd.GtfsManagement.Interfaces.Areas;

// islevi: Area entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Bolge sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS bolge repository sozlesmesini tanimlar.</summary>
public interface IAreaRepository : IExtendedRepository<Area>
{
}
