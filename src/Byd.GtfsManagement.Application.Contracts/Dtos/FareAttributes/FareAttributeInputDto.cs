using System;

namespace Byd.GtfsManagement.Dtos.FareAttributes;

// islevi: fare_attributes.txt duzenlenebilir ucret alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ucret ozelligi ortak istek alanlarini tasir.</summary>
public abstract class FareAttributeInputDto
{
    public Guid? AgencyId { get; set; }
    public string GtfsFareId { get; set; } = default!;
    public decimal Price { get; set; }
    public string CurrencyType { get; set; } = default!;
    public int PaymentMethod { get; set; }
    public int? Transfers { get; set; }
    public int? TransferDuration { get; set; }
}
