using System;

namespace Byd.GtfsManagement.Models.BookingRules;

// islevi: Yeni BookingRule kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS rezervasyon kurali olusturma domain modelini tasir.</summary>
public class BookingRuleCreateModel : BookingRuleInputModel
{
    public Guid FeedId { get; set; }
}
