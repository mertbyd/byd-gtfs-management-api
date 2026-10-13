using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.StopAreas;

namespace Byd.GtfsManagement.Interfaces.StopAreas;

// islevi: StopArea entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Durak bolge iliskisi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS durak bolge iliskisi repository sozlesmesini tanimlar.</summary>
public interface IStopAreaRepository : IExtendedRepository<StopArea>
{
}
