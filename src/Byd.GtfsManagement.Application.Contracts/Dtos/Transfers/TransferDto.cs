using System;

namespace Byd.GtfsManagement.Dtos.Transfers;

// islevi: transfers.txt satirini, bagli durak, guzergah ve seferlerin adlariyla public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class TransferDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }
    public Guid? FromStopId { get; set; }
    public Guid? ToStopId { get; set; }
    public Guid? FromRouteId { get; set; }
    public Guid? ToRouteId { get; set; }
    public Guid? FromTripId { get; set; }
    public Guid? ToTripId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string? FromStopName { get; set; }
    public string? ToStopName { get; set; }
    public string? FromRouteName { get; set; }
    public string? ToRouteName { get; set; }
    public string? FromTripName { get; set; }
    public string? ToTripName { get; set; }
    public int TransferType { get; set; }
    public int? MinTransferTime { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
