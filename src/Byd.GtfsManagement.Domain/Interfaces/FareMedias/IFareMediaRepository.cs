using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FareMedias;

namespace Byd.GtfsManagement.Interfaces.FareMedias;

// islevi: FareMedia entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ucret medyasi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ucret medyasi repository sozlesmesini tanimlar.</summary>
public interface IFareMediaRepository : IExtendedRepository<FareMedia>
{
}
