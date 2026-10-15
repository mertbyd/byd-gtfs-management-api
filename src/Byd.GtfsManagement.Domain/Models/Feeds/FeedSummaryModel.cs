using System;
using Byd.GtfsManagement.Entities.Feeds;

namespace Byd.GtfsManagement.Models.Feeds;

// islevi: Feed'i temel GTFS tablolarindaki kayit sayilari ve takvimlerin kapsadigi servis tarih araligiyla tasir.
// sistemdeki gorevi: Feed ozeti ucunun repository'de tek seferde hesaplanan sonucudur.
public class FeedSummaryModel
{
    public Feed Feed { get; set; } = default!;
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
