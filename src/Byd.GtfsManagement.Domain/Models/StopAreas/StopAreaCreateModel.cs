using System;

namespace Byd.GtfsManagement.Models.StopAreas;

// islevi: Yeni StopArea kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS durak bolge iliskisi olusturma domain modelini tasir.</summary>
public class StopAreaCreateModel : StopAreaInputModel
{
    public Guid FeedId { get; set; }
}
