using Byd.GtfsManagement.Dtos.BookingRules;
using Byd.GtfsManagement.Entities.BookingRules;
using Byd.GtfsManagement.Interfaces.BookingRules;
using Byd.GtfsManagement.Managers.BookingRules;
using Byd.GtfsManagement.Mappers.BookingRules;
using Byd.GtfsManagement.Models.BookingRules;

namespace Byd.GtfsManagement.Services.BookingRules;

// islevi: BookingRule CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS rezervasyon kurali CRUD servisini ortak tabana baglar.</summary>
public class BookingRuleAppService
    : GtfsImportExportAppServiceBase<
        BookingRule, CreateBookingRuleDto, UpdateBookingRuleDto, BookingRuleDto, BookingRuleFilter,
        BookingRuleCreateModel, BookingRuleUpdateModel, BookingRuleManager, IBookingRuleRepository, BookingRuleMapper, BookingRuleImportDto>,
        IBookingRuleAppService
{
}
