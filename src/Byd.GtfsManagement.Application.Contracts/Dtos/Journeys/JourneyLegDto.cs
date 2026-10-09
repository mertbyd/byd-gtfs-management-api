using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.Dtos.Trips;
using Byd.GtfsManagement.Journeys;

namespace Byd.GtfsManagement.Dtos.Journeys;

// islevi: Yolculugun tek adimini turu, duraklari, varsa seferi ve guzergahi, saatleri ve istasyon ici yol bicimiyle tasir.
// sistemdeki gorevi: Yolculuk seceneginin adim listesidir.
public class JourneyLegDto
{
    public JourneyLegKind Kind { get; set; }
    public StopDto FromStop { get; set; } = default!;
    public StopDto ToStop { get; set; } = default!;
    public TripDto? Trip { get; set; }
    public RouteDto? Route { get; set; }
    public string DepartureTime { get; set; } = default!;
    public string ArrivalTime { get; set; } = default!;
    public int DurationSeconds { get; set; }
    public int? PathwayMode { get; set; }
    public string? SignpostedAs { get; set; }
}
