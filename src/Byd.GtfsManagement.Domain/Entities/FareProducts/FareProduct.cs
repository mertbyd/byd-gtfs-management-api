using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareProducts;

// islevi: fare_products.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ucret urunu verilerini Feed altinda tutar.
/// <summary>GTFS ucret urunu kaydini temsil eder.</summary>
public class FareProduct : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS fare_product_id dis kimligi.
    public string GtfsFareProductId { get; internal set; } = default!;

    // Ilgili yolcu kategorisi dis kimligi.
    public string? GtfsRiderCategoryId { get; internal set; }

    // Ilgili ucret medyasi dis kimligi.
    public string? GtfsFareMediaId { get; internal set; }

    // Ucret urunu okunabilir adi.
    public string? FareProductName { get; internal set; }

    // Ucret tutari.
    public decimal Amount { get; internal set; }

    // ISO 4217 para birimi kodu.
    public string Currency { get; internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected FareProduct()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareProduct(Guid id, Guid? tenantId, Guid feedId, string gtfsFareProductId, decimal amount, string currency) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsFareProductId = gtfsFareProductId;
        Amount = amount;
        Currency = currency;
    }
}
