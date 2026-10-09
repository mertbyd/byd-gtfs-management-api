using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Networks;

// islevi: networks.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ulasim agi verilerini Feed altinda tutar.
/// <summary>GTFS ulasim agi kaydini temsil eder.</summary>
public class Network : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS network_id dis kimligi.
    public string GtfsNetworkId { get; internal set; } = default!;

    // Ulasim agi okunabilir adi.
    public string? NetworkName { get; internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad; network_name bossa network_id'dir. Kolon degildir.
    public string Name => NetworkName ?? GtfsNetworkId;

    // EF materialization icin bos kabuk kurar.
    protected Network()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Network(Guid id, Guid? tenantId, Guid feedId, string gtfsNetworkId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsNetworkId = gtfsNetworkId;
    }
}
