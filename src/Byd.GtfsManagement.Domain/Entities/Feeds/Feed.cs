using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Byd.GtfsManagement.Entities.Feeds;

// islevi: Tenant'in bir GTFS veri setini adi ve aktifligiyle saklar.
// sistemdeki gorevi: Tum GTFS tablolari bu kayda baglidir; kayitlar Feed'in tenant kapsamini devralir.
public class Feed : AuditedAggregateRoot<Guid>, IMultiTenant, IHasIsActive
{
    // GTFS veri setini diger musterilerin verilerinden ayiran tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Veri setinin sistemde gorunen adi.
    public string Name { get; internal set; } = default!;

    // Veri setinin tenant'in aktif GTFS'i olup olmadigi.
    public bool IsActive { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Feed()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar.
    public Feed(Guid id, Guid tenantId, string name) : base(id)
    {
        TenantId = tenantId;
        Name = name;
    }

    // Core aktiflik filtresinin okudugu sozlesmeyi public entity setter acmadan uygular.
    bool IHasIsActive.IsActive
    {
        get => IsActive;
        set => IsActive = value;
    }
}
