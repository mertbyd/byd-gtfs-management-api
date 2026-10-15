using Byd.GtfsManagement.Dtos.CalendarDates;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Models.CalendarDates;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.CalendarDates;

// islevi: CalendarDate domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class CalendarDateMapper
    : IGtfsArchiveMapper<CalendarDateImportDto, CalendarDate>, IGtfsCrudMapper<CalendarDate, CreateCalendarDateDto, UpdateCalendarDateDto, CalendarDateDto, CalendarDateCreateModel, CalendarDateUpdateModel>
{
    public partial CalendarDateCreateModel Map(CreateCalendarDateDto input);
    public partial CalendarDateUpdateModel Map(UpdateCalendarDateDto input);
    public partial CalendarDateDto Map(CalendarDate entity);

    // CSV satirini entity'ye esler.
    public partial CalendarDate Map(CalendarDateImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial CalendarDateImportDto MapToImport(CalendarDate entity);
}
