using System;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Bir seferin iki ardisik duragi arasindaki baglantiyi servis gunu saniyesiyle tasir.
// sistemdeki gorevi: Planlayici baglantilari kalkis saatine gore sirayla tarar.
public class JourneyConnectionModel
{
    public Guid TripId { get; set; }
    public Guid FromStopId { get; set; }
    public Guid ToStopId { get; set; }
    public int DepartureSeconds { get; set; }
    public int ArrivalSeconds { get; set; }
}
