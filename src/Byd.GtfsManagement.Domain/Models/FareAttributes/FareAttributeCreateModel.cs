using System;

namespace Byd.GtfsManagement.Models.FareAttributes;

// islevi: Yeni FareAttribute kaydi icin gerekli Feed ve ucret alanlarini tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ucret ozelligi olusturma domain modelini tasir.</summary>
public class FareAttributeCreateModel : FareAttributeInputModel
{
    public Guid FeedId { get; set; }
}
