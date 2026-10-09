using System;

namespace Byd.GtfsManagement.Models.FareAttributes;

// islevi: FareAttribute olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ucret ozelligi ortak domain girdi modelini tasir.</summary>
public abstract class FareAttributeInputModel
{
    public string GtfsFareId { get; set; } = default!;
    public decimal Price { get; set; }
    public string CurrencyType { get; set; } = default!;
    public int PaymentMethod { get; set; }
    public int? Transfers { get; set; }
    public Guid? AgencyId { get; set; }
    public int? TransferDuration { get; set; }
}
