using System;

namespace Byd.GtfsManagement.Models.FareLegRules;

// islevi: Yeni FareLegRule kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ucret etap kurali olusturma domain modelini tasir.</summary>
public class FareLegRuleCreateModel : FareLegRuleInputModel
{
    public Guid FeedId { get; set; }
}
