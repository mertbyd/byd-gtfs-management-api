using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.RiderCategories;

// islevi: rider_categories.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Yolcu kategorisi verilerini Feed altinda tutar.
/// <summary>GTFS yolcu kategorisi kaydini temsil eder.</summary>
public class RiderCategory : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS rider_category_id dis kimligi.
    public string GtfsRiderCategoryId { get; protected internal set; } = default!;

    // Yolcu kategorisi okunabilir adi.
    public string RiderCategoryName { get; protected internal set; } = default!;

    // Varsayilan kategori gostergesi (0 veya 1).
    public int? IsDefaultFareCategory { get; protected internal set; }

    // Uygunluk kriterleri web adresi.
    public string? EligibilityUrl { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected RiderCategory()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public RiderCategory(Guid id, Guid feedId, string gtfsRiderCategoryId) : base(id)
    {
        FeedId = feedId;
        GtfsRiderCategoryId = gtfsRiderCategoryId;
    }
    // Dis anahtarlar disa aktarimda orijinal GTFS kimligine cozulur.
    string? IHasGtfsId.GtfsId => GtfsRiderCategoryId;
}
