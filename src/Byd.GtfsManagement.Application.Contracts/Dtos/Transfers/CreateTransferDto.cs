using System;

namespace Byd.GtfsManagement.Dtos.Transfers;

// mirastan gelen: GTFS aktarma baglari ve turu.
// burada yazilan: hedef Feed.
// islevi: Yeni aktarmanin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS aktarma olusturma istegini tasir.</summary>
public sealed class CreateTransferDto : TransferInputDto
{
    public Guid FeedId { get; set; }
}
