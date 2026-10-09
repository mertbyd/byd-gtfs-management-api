using System.Collections.Generic;
using Byd.GtfsManagement.Dtos.CalendarDates;
using Byd.GtfsManagement.Dtos.Calendars;
using Byd.GtfsManagement.Dtos.Frequencies;
using Byd.GtfsManagement.Dtos.Shapes;
using Byd.GtfsManagement.Dtos.StopTimes;

namespace Byd.GtfsManagement.Dtos.Trips;

// islevi: Seferi duraklari, sikliklari, sekli ve servisinin calisma gunleriyle tek cevapta tasir.
// sistemdeki gorevi: Sefer ekraninin saatleri ve takvimi tek tek sorgulamadan okumasini saglar.
public class TripDetailDto
{
    public TripDto Trip { get; set; } = default!;
    public List<TripStopDto> Stops { get; set; } = [];
    public List<FrequencyDto> Frequencies { get; set; } = [];
    public List<ShapeDto> Shape { get; set; } = [];
    public CalendarDto? Calendar { get; set; }
    public List<CalendarDateDto> CalendarDates { get; set; } = [];
}
