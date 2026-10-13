using System;

namespace Byd.GtfsManagement.Dtos.StopTimes;

// islevi: Durak ziyaretine kalkisin guzergahini, guzergah renklerini ve sefer tabelasini ekler.
// sistemdeki gorevi: Durak kalkis ucunun liste elemanidir; ziyaretin alanlari StopTimeDto'dan gelir.
public class StopDepartureDto : StopTimeDto
{
    public Guid TripRouteId { get; set; }
    public string TripRouteName { get; set; } = default!;
    public string? TripRouteColor { get; set; }
    public string? TripRouteTextColor { get; set; }
    public string? TripHeadsign { get; set; }
}
