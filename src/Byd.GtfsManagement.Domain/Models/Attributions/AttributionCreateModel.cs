using System;

namespace Byd.GtfsManagement.Models.Attributions;

// islevi: Yeni Attribution kaydi icin gerekli Feed ve atif alanlarini tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS atif olusturma domain modelini tasir.</summary>
public class AttributionCreateModel : AttributionInputModel
{
    public Guid FeedId { get; set; }
}
