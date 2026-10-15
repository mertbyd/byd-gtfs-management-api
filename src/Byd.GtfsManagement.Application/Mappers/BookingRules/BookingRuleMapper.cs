using Byd.GtfsManagement.Dtos.BookingRules;
using Byd.GtfsManagement.Entities.BookingRules;
using Byd.GtfsManagement.Models.BookingRules;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.BookingRules;

// islevi: BookingRule DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS rezervasyon kurali DTO ve modellerini esler.</summary>
[Mapper]
public partial class BookingRuleMapper
    : IGtfsArchiveMapper<BookingRuleImportDto, BookingRule>, IGtfsCrudMapper<BookingRule, CreateBookingRuleDto, UpdateBookingRuleDto, BookingRuleDto,
        BookingRuleCreateModel, BookingRuleUpdateModel>
{
    public partial BookingRuleCreateModel Map(CreateBookingRuleDto input);
    public partial BookingRuleUpdateModel Map(UpdateBookingRuleDto input);
    public partial BookingRuleDto Map(BookingRule entity);

    // CSV satirini entity'ye esler.
    public partial BookingRule Map(BookingRuleImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial BookingRuleImportDto MapToImport(BookingRule entity);
}
