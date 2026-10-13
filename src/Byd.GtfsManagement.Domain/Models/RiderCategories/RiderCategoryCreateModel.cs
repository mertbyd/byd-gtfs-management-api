using System;

namespace Byd.GtfsManagement.Models.RiderCategories;

// islevi: Yeni RiderCategory kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS yolcu kategorisi olusturma domain modelini tasir.</summary>
public class RiderCategoryCreateModel : RiderCategoryInputModel
{
    public Guid FeedId { get; set; }
}
