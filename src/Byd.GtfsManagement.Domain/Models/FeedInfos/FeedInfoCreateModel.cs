using System;

namespace Byd.GtfsManagement.Models.FeedInfos;

// islevi: Yeni yayimci bilgisi olusturma alanlarini tasir.
// sistemdeki gorevi: Feed kimligini girdi modeline ekler.
/// <summary>Yeni yayimci bilgisi olusturma modelini tasir.</summary>
public class FeedInfoCreateModel : FeedInfoInputModel
{
    public Guid FeedId { get; set; }
}
