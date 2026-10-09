using System;

namespace Byd.GtfsManagement.Models.FareRules;

// islevi: Yeni FareRule kaydi icin gerekli Feed ve kural alanlarini tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ucret kurali olusturma domain modelini tasir.</summary>
public class FareRuleCreateModel : FareRuleInputModel
{
    public Guid FeedId { get; set; }
}
