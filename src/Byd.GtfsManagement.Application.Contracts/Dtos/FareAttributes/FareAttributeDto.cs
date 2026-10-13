using System;

namespace Byd.GtfsManagement.Dtos.FareAttributes;

// islevi: fare_attributes.txt satirini, bagli ajansin adi ve ucret alanlariyla public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class FareAttributeDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid? AgencyId { get; set; }
    public string? AgencyName { get; set; }
    public string GtfsFareId { get; set; } = default!;
    public decimal Price { get; set; }
    public string CurrencyType { get; set; } = default!;
    public int PaymentMethod { get; set; }
    public int? Transfers { get; set; }
    public int? TransferDuration { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
