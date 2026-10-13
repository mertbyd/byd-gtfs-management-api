using System;

namespace Byd.GtfsManagement.Dtos.FeedInfos;

// islevi: Yeni yayimci bilgisi olusturma HTTP istegini tasir.
// sistemdeki gorevi: Gerekli Feed kimligini girdi sozlesmesine ekler.
/// <summary>Yeni yayimci bilgisi olusturma DTO alanlarini tasir.</summary>
public class CreateFeedInfoDto : FeedInfoInputDto
{
    public Guid FeedId { get; set; }
}
