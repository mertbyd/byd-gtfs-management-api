using System;

namespace Byd.GtfsManagement.Models.Transfers;

// mirastan gelen: GTFS aktarma alanlari.
// burada yazilan: degismeyen Feed.
// islevi: Yeni aktarmanin Feed kimligini tasir.
// sistemdeki gorevi: Manager'in Feed'den tenant kapsamini kurmasini saglar.
/// <summary>GTFS aktarma olusturma modelini tasir.</summary>
public sealed class TransferCreateModel : TransferInputModel
{
    public Guid FeedId { get; set; }
}
