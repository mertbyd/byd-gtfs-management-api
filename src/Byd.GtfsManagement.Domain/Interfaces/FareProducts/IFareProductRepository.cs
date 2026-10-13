using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FareProducts;

namespace Byd.GtfsManagement.Interfaces.FareProducts;

// islevi: FareProduct entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ucret urunu sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ucret urunu repository sozlesmesini tanimlar.</summary>
public interface IFareProductRepository : IExtendedRepository<FareProduct>
{
}
