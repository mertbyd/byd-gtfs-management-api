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
// sistemdeki gorevi: GTFS guzergahini Feed kapsaminda tutar.
public class Route : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS agency_id; Feed'de tek ajans varsa bos birakilabilir.
    public Guid? AgencyId { get; protected internal set; }

    // GTFS route_type lookup satirinin kimligi.
    public int RouteTypeId { get; protected internal set; }

    // Istege bagli continuous_pickup lookup satirinin kimligi.
    public int? ContinuousPickupId { get; protected internal set; }

    // Istege bagli continuous_drop_off lookup satirinin kimligi.
    public int? ContinuousDropOffId { get; protected internal set; }

    // GTFS network_id; guzergahin bagli oldugu ucret aginin kimligi.
    public Guid? NetworkId { get; protected internal set; }

    // Istege bagli cemv_support lookup satirinin kimligi.
    public int? CemvSupportId { get; protected internal set; }

    // GTFS route_id; veri seti icinde guzergahi tanimlayan kimlik.
    public string GtfsRouteId { get; protected internal set; } = default!;

    // GTFS route_short_name; hat numarasi gibi kisa tanim.
    public string? ShortName { get; protected internal set; }

    // GTFS route_long_name; guzergahin tam adi.
    public string? LongName { get; protected internal set; }

    // GTFS route_desc; guzergahi aciklayan ek metin.
    public string? Description { get; protected internal set; }

    // GTFS route_url; guzergaha ait bilgi sayfasinin adresi.
    public string? Url { get; protected internal set; }

    // GTFS route_color; alti haneli onaltilik zemin rengi.
    public string? Color { get; protected internal set; }

    // GTFS route_text_color; alti haneli onaltilik yazi rengi.
    public string? TextColor { get; protected internal set; }

    // GTFS route_sort_order; guzergahlarin yolcuya gosterilme sirasi.
    public int? SortOrder { get; protected internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad: kisa ad, yoksa uzun ad; ikisi de yoksa route_id. Kolon degildir.
    public string Name => ShortName ?? LongName ?? GtfsRouteId;

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Guzergahi isleten ajans.
    public Agency? Agency { get; protected internal set; }
    // Guzergahin ulasim turu.
    public RouteType RouteType { get; protected internal set; } = default!;
    // Guzergah boyunca durak disi binis kurali.
    public ContinuousPickupDropOff? ContinuousPickup { get; protected internal set; }
    // Guzergah boyunca durak disi inis kurali.
    public ContinuousPickupDropOff? ContinuousDropOff { get; protected internal set; }
    // Guzergahin bagli oldugu ucret agi.
    public Network? Network { get; protected internal set; }
    // Guzergahta temassiz banka karti ile odeme destegi.
    public CemvSupport? CemvSupport { get; protected internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Route()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Route(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }

    // Disa aktarimin bu kayda bakan yabanci anahtarlari cevirdigi GTFS kimligini public setter acmadan verir.
    string? IHasGtfsId.GtfsId => GtfsRouteId;
}
