using System;

namespace Byd.GtfsManagement.Models.Shapes;

// islevi: Yeni bir sekil noktasi olusturma alanlarini tasir.
// sistemdeki gorevi: Feed kimligini girdi alanlarina ekler.
/// <summary>Yeni sekil noktasi olusturma modelini tasir.</summary>
public class ShapeCreateModel : ShapeInputModel
{
    public Guid FeedId { get; set; }
}
