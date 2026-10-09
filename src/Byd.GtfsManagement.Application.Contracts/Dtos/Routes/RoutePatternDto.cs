using System.Collections.Generic;
using Byd.GtfsManagement.Dtos.Shapes;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Dtos.Trips;

namespace Byd.GtfsManagement.Dtos.Routes;

// islevi: Guzergahin ayni yon ve tabelali seferlerini ornek sefer, sefer sayisi, ornek seferin duraklari ve sekliyle tasir.
// sistemdeki gorevi: Guzergah detay cevabinin sefer kaliplaridir.
public class RoutePatternDto
{
    public TripDto Trip { get; set; } = default!;
    public int TripCount { get; set; }
    public List<TripStopDto> Stops { get; set; } = [];
    public List<ShapeDto> Shape { get; set; } = [];
}
