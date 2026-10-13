using System;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Byd.GtfsManagement.Entities.Lookup.RouteTypes;
using Byd.GtfsManagement.Entities.Networks;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Routes;

// islevi: routes.txt icindeki yolcuya tek bir hizmet olarak sunulan guzergahi saklar.
// sistemdeki gorevi: GTFS guzergahini tenant ve Feed kapsaminda tutar.
public class Route : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS agency_id; Feed'de tek ajans varsa bos birakilabilir.
    public Guid? AgencyId { get; internal set; }

    // Guzergahi isleten ajans.
    public Agency? Agency { get; internal set; }

    // GTFS route_type lookup satirinin kimligi.
    public int RouteTypeId { get; internal set; }

    // Guzergahin ulasim turu.
    public RouteType RouteType { get; internal set; } = default!;

    // Istege bagli continuous_pickup lookup satirinin kimligi.
    public int? ContinuousPickupId { get; internal set; }

    // Guzergah boyunca durak disi binis kurali.
    public ContinuousPickupDropOff? ContinuousPickup { get; internal set; }

    // Istege bagli continuous_drop_off lookup satirinin kimligi.
    public int? ContinuousDropOffId { get; internal set; }

    // Guzergah boyunca durak disi inis kurali.
    public ContinuousPickupDropOff? ContinuousDropOff { get; internal set; }

    // GTFS network_id; guzergahin bagli oldugu ucret aginin kimligi.
    public Guid? NetworkId { get; internal set; }

    // Guzergahin bagli oldugu ucret agi.
    public Network? Network { get; internal set; }

    // Istege bagli cemv_support lookup satirinin kimligi.
    public int? CemvSupportId { get; internal set; }

    // Guzergahta temassiz banka karti ile odeme destegi.
    public CemvSupport? CemvSupport { get; internal set; }

    // GTFS route_id; veri seti icinde guzergahi tanimlayan kimlik.
    public string GtfsRouteId { get; internal set; } = default!;

    // GTFS route_short_name; hat numarasi gibi kisa tanim.
    public string? ShortName { get; internal set; }

    // GTFS route_long_name; guzergahin tam adi.
    public string? LongName { get; internal set; }

    // GTFS route_desc; guzergahi aciklayan ek metin.
    public string? Description { get; internal set; }

    // GTFS route_url; guzergaha ait bilgi sayfasinin adresi.
    public string? Url { get; internal set; }

    // GTFS route_color; alti haneli onaltilik zemin rengi.
    public string? Color { get; internal set; }

    // GTFS route_text_color; alti haneli onaltilik yazi rengi.
    public string? TextColor { get; internal set; }

    // GTFS route_sort_order; guzergahlarin yolcuya gosterilme sirasi.
    public int? SortOrder { get; internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad: kisa ad, yoksa uzun ad; ikisi de yoksa route_id. Kolon degildir.
    public string Name => ShortName ?? LongName ?? GtfsRouteId;

    // EF materialization icin bos kabuk kurar.
    protected Route()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Route(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
