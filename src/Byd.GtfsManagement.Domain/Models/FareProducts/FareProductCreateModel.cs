using System;

namespace Byd.GtfsManagement.Models.FareProducts;

// islevi: Yeni FareProduct kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ucret urunu olusturma domain modelini tasir.</summary>
public class FareProductCreateModel : FareProductInputModel
{
    public Guid FeedId { get; set; }
}
