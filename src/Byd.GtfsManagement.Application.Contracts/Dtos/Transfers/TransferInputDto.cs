using System;

namespace Byd.GtfsManagement.Dtos.Transfers;

// islevi: transfers.txt duzenlenebilir bag ve aktarma alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS aktarma ortak istek alanlarini tasir.</summary>
public abstract class TransferInputDto
{
    public Guid? FromStopId { get; set; }
    public Guid? ToStopId { get; set; }
    public Guid? FromRouteId { get; set; }
    public Guid? ToRouteId { get; set; }
    public Guid? FromTripId { get; set; }
    public Guid? ToTripId { get; set; }
    public int? TransferType { get; set; }
    public int? MinTransferTime { get; set; }
}
