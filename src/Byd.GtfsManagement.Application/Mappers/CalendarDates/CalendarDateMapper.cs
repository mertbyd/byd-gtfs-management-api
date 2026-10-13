using Byd.GtfsManagement.Dtos.CalendarDates;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Models.CalendarDates;
using Byd.GtfsManagement.Mappers;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.CalendarDates;

// islevi: CalendarDate domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class CalendarDateMapper
    : IGtfsCrudMapper<CalendarDate, CreateCalendarDateDto, UpdateCalendarDateDto, CalendarDateDto, CalendarDateCreateModel, CalendarDateUpdateModel>
{
    public partial CalendarDateCreateModel Map(CreateCalendarDateDto input);
    public partial CalendarDateUpdateModel Map(UpdateCalendarDateDto input);
    public partial CalendarDateDto Map(CalendarDate entity);
}
