using System.Collections.Generic;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Entities.Calendars;
using Byd.GtfsManagement.Entities.Frequencies;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Entities.Trips;

namespace Byd.GtfsManagement.Models.Trips;

// islevi: Bir seferi duraklari, sikliklari, sekli ve calisma gunleriyle birlikte tasir.
// sistemdeki gorevi: Sefer detay ucunun bilesik sonucudur.
public class TripDetailModel
{
    public Trip Trip { get; set; } = default!;
    public List<StopTime> Stops { get; set; } = [];
    public List<Frequency> Frequencies { get; set; } = [];
    public List<Shape> Shape { get; set; } = [];
    public Calendar? Calendar { get; set; }
    public List<CalendarDate> CalendarDates { get; set; } = [];
}
