using System;

namespace Byd.GtfsManagement.Models.Networks;

// islevi: Yeni Network kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ulasim agi olusturma domain modelini tasir.</summary>
public class NetworkCreateModel : NetworkInputModel
{
    public Guid FeedId { get; set; }
}
