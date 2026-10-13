using System;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Attributions;

// islevi: attributions.txt veri seti katilimci, yayimci, operator veya yetkili atiflarini saklar.
// sistemdeki gorevi: Kurum ve rol atiflarini Feed, ajans, guzergah ve sefer kapsamlariyla tutar.
/// <summary>GTFS atif kaydini temsil eder.</summary>
public class Attribution : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Bagli ajans kimligi.
    public Guid? AgencyId { get; internal set; }

    // Bagli ajans kaydi.
    public Agency? Agency { get; internal set; }

    // Bagli guzergah kimligi.
    public Guid? RouteId { get; internal set; }

    // Bagli guzergah kaydi.
    public Route? Route { get; internal set; }

    // Bagli sefer kimligi.
    public Guid? TripId { get; internal set; }

    // Bagli sefer kaydi.
    public Trip? Trip { get; internal set; }

    // GTFS attribution_id; atfi tanimlayan dis kimlik.
    public string? GtfsAttributionId { get; internal set; }

    // Kurumun tam adi.
    public string OrganizationName { get; internal set; } = default!;

    // GTFS is_producer; veri setini ureten kurum mu (0/1).
    public int? IsProducer { get; internal set; }

    // GTFS is_operator; ulasim operatoru mu (0/1).
    public int? IsOperator { get; internal set; }

    // GTFS is_authority; transit otoritesi mi (0/1).
    public int? IsAuthority { get; internal set; }

    // Atif web sitesi adresi.
    public string? AttributionUrl { get; internal set; }

    // Atif e-posta adresi.
    public string? AttributionEmail { get; internal set; }

    // Atif telefon numarasi.
    public string? AttributionPhone { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Attribution()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Attribution(Guid id, Guid? tenantId, Guid feedId, string organizationName) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        OrganizationName = organizationName;
    }
}
