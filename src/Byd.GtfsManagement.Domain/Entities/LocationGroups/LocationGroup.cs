using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.LocationGroups;

// islevi: location_groups.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Konum grubu verilerini Feed altinda tutar.
/// <summary>GTFS konum grubu kaydini temsil eder.</summary>
public class LocationGroup : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS location_group_id dis kimligi.
    public string GtfsLocationGroupId { get; internal set; } = default!;

    // Konum grubu okunabilir adi.
    public string? LocationGroupName { get; internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad; location_group_name bossa location_group_id'dir. Kolon degildir.
    public string Name => LocationGroupName ?? GtfsLocationGroupId;

    // EF materialization icin bos kabuk kurar.
    protected LocationGroup()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public LocationGroup(Guid id, Guid? tenantId, Guid feedId, string gtfsLocationGroupId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsLocationGroupId = gtfsLocationGroupId;
    }
}
