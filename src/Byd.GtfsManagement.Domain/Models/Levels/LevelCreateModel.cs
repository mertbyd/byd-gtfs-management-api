using System;

namespace Byd.GtfsManagement.Models.Levels;

// islevi: Yeni kat olusturma alanlarini tasir.
// sistemdeki gorevi: Feed kimligini girdi modeline ekler.
/// <summary>Yeni kat olusturma modelini tasir.</summary>
public class LevelCreateModel : LevelInputModel
{
    public Guid FeedId { get; set; }
}
