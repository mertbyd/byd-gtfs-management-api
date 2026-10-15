using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Areas;

// islevi: areas.txt icindeki GTFS cografi bolgelerini saklar.
// sistemdeki gorevi: Durak bolgelerini ve Fares V2 ucret bolgelerini Feed altinda tutar.
/// <summary>GTFS bolge kaydini temsil eder.</summary>
public class Area : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS area_id; bolgeyi tanimlayan dis kimlik.
    public string GtfsAreaId { get; protected internal set; } = default!;

    // Bolgenin okunabilir adi.
    public string? AreaName { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Area()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Area(Guid id, Guid feedId, string gtfsAreaId) : base(id)
    {
        FeedId = feedId;
        GtfsAreaId = gtfsAreaId;
    }
    // Dis anahtarlar disa aktarimda orijinal GTFS kimligine cozulur.
    string? IHasGtfsId.GtfsId => GtfsAreaId;
}
