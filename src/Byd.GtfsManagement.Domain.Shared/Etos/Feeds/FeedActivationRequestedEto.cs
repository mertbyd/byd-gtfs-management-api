using System;

namespace Byd.GtfsManagement.Etos.Feeds;

/// <summary>
/// Bir Feed'in aktif GTFS yapilmasinin istendigini bildiren yerel olay.
/// </summary>
// islevi: Aktiflestirilecek Feed'in kimligini tasir.
// sistemdeki gorevi: Aktiflestirme ucu ile aktif Feed'i degistiren handler arasindaki surec ici sozlesmedir. Dagitik degildir;
// handler istekle ayni transaction'da calisir.
public class FeedActivationRequestedEto
{
    public Guid TenantId { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid FeedId { get; set; }
}
