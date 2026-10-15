using System;
using Byd.GtfsManagement.Consts;
using Volo.Abp.EventBus;

namespace Byd.GtfsManagement.Etos.Feeds;

/// <summary>
/// Dogrulanmis Feed'in GTFS tablolarina yazilmasini ister.
/// </summary>
// islevi: Entegrasyon bekleyen Feed'in tenant ve kayit kimligini tasir.
// sistemdeki gorevi: Dogrulama tamamlandiginda entegrasyon handler'ini kuyruk uzerinden tetikler.
[EventName(FeedEventNames.IntegrationRequested)]
public class FeedIntegrationRequestedEto
{
    public Guid TenantId { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid FeedId { get; set; }
}
