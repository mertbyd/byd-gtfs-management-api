using System;

namespace Byd.GtfsManagement.Models.Pathways;

// islevi: Yeni yuruyus yolu olusturma alanlarini tasir.
// sistemdeki gorevi: Feed kimligini girdi modeline ekler.
/// <summary>Yeni yuruyus yolu olusturma modelini tasir.</summary>
public class PathwayCreateModel : PathwayInputModel
{
    public Guid FeedId { get; set; }
}
