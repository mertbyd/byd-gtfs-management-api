using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Byd.GtfsManagement.Entities.Lookup.StopAccesses;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Stops;

// islevi: stops.txt icindeki durak, istasyon, giris, ara dugum ve binis alani kaydini saklar.
// sistemdeki gorevi: GTFS konum hiyerarsisini Feed kapsaminda tutar.
public class Stop : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Istege bagli location_type lookup satirinin kimligi; bos ise durak veya peron kabul edilir.
    public int? LocationTypeId { get; protected internal set; }

    // Istege bagli wheelchair_boarding lookup satirinin kimligi.
    public int? WheelchairBoardingId { get; protected internal set; }

    // GTFS parent_station; kaydin bagli oldugu istasyonun kimligi.
    public Guid? ParentStationId { get; protected internal set; }

    // GTFS level_id; duragin bulundugu katin kimligi.
    public Guid? LevelId { get; protected internal set; }

    // Istege bagli stop_access lookup satirinin kimligi; bos ise istasyonun erisim yolu gecerlidir.
    public int? StopAccessId { get; protected internal set; }

    // GTFS stop_id; veri seti icinde kaydi tanimlayan kimlik.
    public string GtfsStopId { get; protected internal set; } = default!;

    // GTFS zone_id; ucretlendirme bolgesi kimligi.
    public string? ZoneId { get; protected internal set; }

    // GTFS stop_code; yolcuya duyurulan kisa durak kodu.
    public string? Code { get; protected internal set; }

    // GTFS stop_name; yolcuya gosterilen durak adi.
    public string? Name { get; protected internal set; }

    // GTFS tts_stop_name; seslendirme icin okunusu duzeltilmis durak adi.
    public string? TtsName { get; protected internal set; }

    // GTFS stop_desc; duragin konumunu aciklayan ek metin.
    public string? Description { get; protected internal set; }

    // GTFS stop_lat; WGS84 enlem degeri.
    public double? Latitude { get; protected internal set; }

    // GTFS stop_lon; WGS84 boylam degeri.
    public double? Longitude { get; protected internal set; }

    // GTFS stop_url; duraga ait bilgi sayfasinin adresi.
    public string? Url { get; protected internal set; }

    // GTFS stop_timezone; durak saatlerinin yorumlandigi saat dilimi.
    public string? Timezone { get; protected internal set; }

    // GTFS platform_code; istasyon icindeki peron tanimi.
    public string? PlatformCode { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Kaydin GTFS konum turu.
    public LocationType? LocationType { get; protected internal set; }
    // Duraga tekerlekli sandalye ile erisim durumu.
    public WheelchairBoarding? WheelchairBoarding { get; protected internal set; }
    // Kaydin bagli oldugu istasyon.
    public Stop? ParentStation { get; protected internal set; }
    // Duragin bulundugu kat.
    public Level? Level { get; protected internal set; }
    // Duraga sokaktan dogrudan mi istasyon uzerinden mi erisildigi.
    public StopAccess? StopAccess { get; protected internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Stop()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Stop(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }

    // Disa aktarimin bu kayda bakan yabanci anahtarlari cevirdigi GTFS kimligini public setter acmadan verir.
    string? IHasGtfsId.GtfsId => GtfsStopId;
}
