using System;

namespace Byd.GtfsManagement.Models.StopTimes;

// islevi: Planlayicinin okudugu tek durak ziyaretini sefer, durak, sira ve saatleriyle tasir.
// sistemdeki gorevi: Ardisik ziyaretlerden yolculuk baglantilari kurulur.
public class StopVisitModel
{
    public Guid TripId { get; set; }
    public Guid StopId { get; set; }
    public int StopSequence { get; set; }
    public string ArrivalTime { get; set; } = default!;
    public string DepartureTime { get; set; } = default!;
}
