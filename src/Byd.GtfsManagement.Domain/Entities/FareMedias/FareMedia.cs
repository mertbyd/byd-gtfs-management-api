using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareMedias;

// islevi: fare_media.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ucret medyasi verilerini Feed altinda tutar.
/// <summary>GTFS ucret medyasi kaydini temsil eder.</summary>
public class FareMedia : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS fare_media_id dis kimligi.
    public string GtfsFareMediaId { get; internal set; } = default!;

    // Odeme medyasi okunabilir adi.
    public string? FareMediaName { get; internal set; }

    // Odeme medyasi tipi (0: Yok, 1: Kagit, 2: Kart, 3: Temassiz EMV, 4: Mobil).
    public int FareMediaType { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected FareMedia()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareMedia(Guid id, Guid? tenantId, Guid feedId, string gtfsFareMediaId, int fareMediaType) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsFareMediaId = gtfsFareMediaId;
        FareMediaType = fareMediaType;
    }
}
