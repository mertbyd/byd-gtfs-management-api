using System;

namespace Byd.GtfsManagement.Dtos.StopAreas;

// islevi: Yeni durak bolge iliskisi kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS durak bolge iliskisi olusturma istegini tasir.</summary>
public sealed class CreateStopAreaDto : StopAreaInputDto
{
    public Guid FeedId { get; set; }
}
