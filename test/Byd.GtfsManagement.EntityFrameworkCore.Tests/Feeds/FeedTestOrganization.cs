using System;

namespace Byd.GtfsManagement.EntityFrameworkCore.Feeds;

// islevi: Bir organizasyonun aktif ve pasif Feed test kimliklerini tasir.
// sistemdeki gorevi: Birden fazla Feed icin beklenen yayin secimini tanimlar.
internal sealed record FeedTestOrganization(
    Guid TenantId,
    Guid OrganizationId,
    FeedTestFeed Active,
    FeedTestFeed Inactive);
