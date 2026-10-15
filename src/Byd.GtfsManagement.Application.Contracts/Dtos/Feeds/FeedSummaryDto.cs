using System;

namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Feed'i temel GTFS tablolarindaki kayit sayilari ve takvimlerin kapsadigi servis tarih araligiyla tasir.
// sistemdeki gorevi: Dashboard'un veri seti kartini tek istekte doldurur; dogrulama raporu ayrinti cevabinda doner.
public class FeedSummaryDto
{
    public FeedDto Feed { get; set; } = default!;
    public int AgencyCount { get; set; }
    public int StopCount { get; set; }
    public int RouteCount { get; set; }
    public int TripCount { get; set; }
    public int StopTimeCount { get; set; }
    public int ServiceCount { get; set; }
    public int ShapeCount { get; set; }
    public DateOnly? ServiceStartDate { get; set; }
    public DateOnly? ServiceEndDate { get; set; }
}
