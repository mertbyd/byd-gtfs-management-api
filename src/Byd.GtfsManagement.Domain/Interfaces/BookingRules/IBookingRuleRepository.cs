using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.BookingRules;

namespace Byd.GtfsManagement.Interfaces.BookingRules;

// islevi: BookingRule entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Rezervasyon kurali sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS rezervasyon kurali repository sozlesmesini tanimlar.</summary>
public interface IBookingRuleRepository : IExtendedRepository<BookingRule>
{
}
