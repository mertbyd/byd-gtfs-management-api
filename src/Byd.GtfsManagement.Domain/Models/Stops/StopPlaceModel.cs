using System;

namespace Byd.GtfsManagement.Models.Stops;

// islevi: Yolculuk planlayicisinin ihtiyac duydugu durak konumunu, istasyon bagini ve GTFS konum turu kodunu tasir.
// sistemdeki gorevi: Feed'in tum duraklarini tek hafif sorguda okuyup yurume ve istasyon ici yollari kurmaya yarar.
public class StopPlaceModel
{
    public Guid Id { get; set; }
    public Guid? ParentStationId { get; set; }
    public int LocationTypeCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
