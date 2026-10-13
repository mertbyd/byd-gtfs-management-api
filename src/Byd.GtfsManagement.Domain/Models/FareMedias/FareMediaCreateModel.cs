using System;

namespace Byd.GtfsManagement.Models.FareMedias;

// islevi: Yeni FareMedia kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ucret medyasi olusturma domain modelini tasir.</summary>
public class FareMediaCreateModel : FareMediaInputModel
{
    public Guid FeedId { get; set; }
}
