using Byd.GtfsManagement.Dtos.BookingRules;

namespace Byd.GtfsManagement.Services.BookingRules;

// islevi: BookingRule CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS rezervasyon kurali CRUD API sozlesmesini bildirir.</summary>
public interface IBookingRuleAppService
    : IGtfsCrudAppService<CreateBookingRuleDto, UpdateBookingRuleDto, BookingRuleDto, BookingRuleFilter>
{
}
