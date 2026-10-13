using System;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Journeys;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Yolculugun tek adimini turu, duraklari, varsa seferi ve guzergahi, saatleri ve istasyon ici yol bicimiyle tasir.
// sistemdeki gorevi: Planlayici adimi kimliklerle kurar; duraklar, sefer ve guzergah cevapta gosterilmek uzere sonradan doldurulur.
public class JourneyLegModel
{
    public JourneyLegKind Kind { get; set; }
    public Guid FromStopId { get; set; }
    public Stop FromStop { get; set; } = default!;
    public Guid ToStopId { get; set; }
    public Stop ToStop { get; set; } = default!;
    public Guid? TripId { get; set; }
    public Trip? Trip { get; set; }
    public Route? Route { get; set; }
    public string DepartureTime { get; set; } = default!;
    public string ArrivalTime { get; set; } = default!;
    public int DurationSeconds { get; set; }
    public int? PathwayMode { get; set; }
    public string? SignpostedAs { get; set; }
}
