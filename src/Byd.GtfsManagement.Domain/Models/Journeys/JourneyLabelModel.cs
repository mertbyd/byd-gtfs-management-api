using System;
using System.Collections.Generic;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Bir duraga belli aktarma sayisiyla en erken nasil varildigini tasir.
// sistemdeki gorevi: Planlayici secenekleri bu etiketlerin onceki duraklarini izleyerek geri kurar.
public class JourneyLabelModel
{
    public int ArrivalSeconds { get; set; }
    public int DepartureSeconds { get; set; }
    public Guid? PreviousStopId { get; set; }
    public Guid? TripId { get; set; }
    public List<JourneyFootpathModel> Hops { get; set; } = [];
}
