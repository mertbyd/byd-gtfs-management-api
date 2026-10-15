using Byd.GtfsManagement.Dtos.Calendars;
using Byd.GtfsManagement.Entities.Calendars;
using Byd.GtfsManagement.Models.Calendars;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Calendars;

// islevi: Calendar domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class CalendarMapper
    : IGtfsArchiveMapper<CalendarImportDto, Calendar>, IGtfsCrudMapper<Calendar, CreateCalendarDto, UpdateCalendarDto, CalendarDto, CalendarCreateModel, CalendarUpdateModel>
{
    public partial CalendarCreateModel Map(CreateCalendarDto input);
    public partial CalendarUpdateModel Map(UpdateCalendarDto input);
    public partial CalendarDto Map(Calendar entity);

    // CSV satirini entity'ye esler.
    public partial Calendar Map(CalendarImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial CalendarImportDto MapToImport(Calendar entity);
}
