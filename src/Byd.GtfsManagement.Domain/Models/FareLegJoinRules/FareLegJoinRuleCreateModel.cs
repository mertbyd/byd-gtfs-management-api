using System;

namespace Byd.GtfsManagement.Models.FareLegJoinRules;

// islevi: Yeni FareLegJoinRule kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ucret etap birlestirme kurali olusturma domain modelini tasir.</summary>
public class FareLegJoinRuleCreateModel : FareLegJoinRuleInputModel
{
    public Guid FeedId { get; set; }
}
