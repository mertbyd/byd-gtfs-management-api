using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Networks;

// islevi: networks.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ulasim agi verilerini Feed altinda tutar.
/// <summary>GTFS ulasim agi kaydini temsil eder.</summary>
public class Network : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS network_id dis kimligi.
    public string GtfsNetworkId { get; protected internal set; } = default!;

    // Ulasim agi okunabilir adi.
    public string? NetworkName { get; protected internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad; network_name bossa network_id'dir. Kolon degildir.
    public string Name => NetworkName ?? GtfsNetworkId;

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Network()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Network(Guid id, Guid feedId, string gtfsNetworkId) : base(id)
    {
        FeedId = feedId;
        GtfsNetworkId = gtfsNetworkId;
    }
    // Dis anahtarlar disa aktarimda orijinal GTFS kimligine cozulur.
    string? IHasGtfsId.GtfsId => GtfsNetworkId;
}
