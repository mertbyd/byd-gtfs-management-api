using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Areas;

// islevi: areas.txt icindeki GTFS cografi bolgelerini saklar.
// sistemdeki gorevi: Durak bolgelerini ve Fares V2 ucret bolgelerini Feed altinda tutar.
/// <summary>GTFS bolge kaydini temsil eder.</summary>
public class Area : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS area_id; bolgeyi tanimlayan dis kimlik.
    public string GtfsAreaId { get; internal set; } = default!;

    // Bolgenin okunabilir adi.
    public string? AreaName { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Area()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Area(Guid id, Guid? tenantId, Guid feedId, string gtfsAreaId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsAreaId = gtfsAreaId;
    }
}
