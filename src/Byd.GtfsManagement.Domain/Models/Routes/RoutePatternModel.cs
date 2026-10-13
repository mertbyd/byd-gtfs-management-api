using System.Collections.Generic;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Entities.Trips;

namespace Byd.GtfsManagement.Models.Routes;

// islevi: Guzergahin ayni yon ve tabelali seferlerini ornek seferi, sefer sayisi, ornek seferin duraklari ve sekliyle tasir.
// sistemdeki gorevi: Guzergah detay ucunun sefer kalibidir; ornek sefer en kucuk trip_id'li seferdir.
public class RoutePatternModel
{
    public Trip Trip { get; set; } = default!;
    public int TripCount { get; set; }
    public List<StopTime> Stops { get; set; } = [];
    public List<Shape> Shape { get; set; } = [];
}
