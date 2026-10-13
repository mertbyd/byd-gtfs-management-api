using System;

namespace Byd.GtfsManagement.Models.LocationGroups;

// islevi: Yeni LocationGroup kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS konum grubu olusturma domain modelini tasir.</summary>
public class LocationGroupCreateModel : LocationGroupInputModel
{
    public Guid FeedId { get; set; }
}
