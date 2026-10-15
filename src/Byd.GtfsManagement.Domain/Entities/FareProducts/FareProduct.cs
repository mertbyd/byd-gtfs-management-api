using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareProducts;

// islevi: fare_products.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ucret urunu verilerini Feed altinda tutar.
/// <summary>GTFS ucret urunu kaydini temsil eder.</summary>
public class FareProduct : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS fare_product_id dis kimligi.
    public string GtfsFareProductId { get; protected internal set; } = default!;

    // Ilgili yolcu kategorisi dis kimligi.
    public string? GtfsRiderCategoryId { get; protected internal set; }

    // Ilgili ucret medyasi dis kimligi.
    public string? GtfsFareMediaId { get; protected internal set; }

    // Ucret urunu okunabilir adi.
    public string? FareProductName { get; protected internal set; }

    // Ucret tutari.
    public decimal Amount { get; protected internal set; }

    // ISO 4217 para birimi kodu.
    public string Currency { get; protected internal set; } = default!;

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected FareProduct()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareProduct(Guid id, Guid feedId, string gtfsFareProductId, decimal amount, string currency) : base(id)
    {
        FeedId = feedId;
        GtfsFareProductId = gtfsFareProductId;
        Amount = amount;
        Currency = currency;
    }
    // Dis anahtarlar disa aktarimda orijinal GTFS kimligine cozulur.
    string? IHasGtfsId.GtfsId => GtfsFareProductId;
}
