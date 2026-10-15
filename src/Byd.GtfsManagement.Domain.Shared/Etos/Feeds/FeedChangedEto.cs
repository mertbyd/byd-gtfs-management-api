using System;
using Byd.GtfsManagement.Consts;
using Volo.Abp.EventBus;

namespace Byd.GtfsManagement.Etos.Feeds;

/// <summary>GTFS Feed icerigi degistiginde diger modulleri bilgilendirir.</summary>
[EventName(FeedEventNames.Changed)]
public class FeedChangedEto
{
    public Guid TenantId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid FeedId { get; set; }
}
