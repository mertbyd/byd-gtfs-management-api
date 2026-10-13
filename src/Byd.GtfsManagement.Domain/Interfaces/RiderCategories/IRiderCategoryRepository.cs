using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.RiderCategories;

namespace Byd.GtfsManagement.Interfaces.RiderCategories;

// islevi: RiderCategory entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Yolcu kategorisi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS yolcu kategorisi repository sozlesmesini tanimlar.</summary>
public interface IRiderCategoryRepository : IExtendedRepository<RiderCategory>
{
}
