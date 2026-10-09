using System;

namespace Byd.GtfsManagement.Models.Areas;

// islevi: Yeni Area kaydi icin gerekli Feed ve bolge alanlarini tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS bolge olusturma domain modelini tasir.</summary>
public class AreaCreateModel : AreaInputModel
{
    public Guid FeedId { get; set; }
}
