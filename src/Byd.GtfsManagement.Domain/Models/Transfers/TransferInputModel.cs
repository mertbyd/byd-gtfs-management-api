using System;

namespace Byd.GtfsManagement.Models.Transfers;

// islevi: transfers.txt aktarma baglarini ve degistirilebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update Manager akislari icin ortak domain girdisi sunar.
/// <summary>GTFS aktarma domain girdisini tasir.</summary>
public abstract class TransferInputModel
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
