using System;

namespace Byd.GtfsManagement.Dtos.Levels;

// islevi: Yeni kat olusturma HTTP istegini tasir.
// sistemdeki gorevi: Gerekli Feed kimligini girdi sozlesmesine ekler.
/// <summary>Yeni kat olusturma DTO alanlarini tasir.</summary>
public class CreateLevelDto : LevelInputDto
{
    public Guid FeedId { get; set; }
}
