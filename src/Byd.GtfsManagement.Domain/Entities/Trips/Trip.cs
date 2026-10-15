using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Lookup.BikesAlloweds;
using Byd.GtfsManagement.Entities.Lookup.CarsAlloweds;
using Byd.GtfsManagement.Entities.Lookup.TripDirections;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Trips;

// islevi: trips.txt icindeki bir guzergahin tek bir yolculugunu saklar.
// sistemdeki gorevi: Guzergah ile servis takvimini birlestirip stop_times kayitlarinin baglandigi seferi tutar.
public class Trip : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS route_id; seferin isledigi guzergahin kimligi.
    public Guid RouteId { get; protected internal set; }

    // GTFS service_id; seferin calistigi takvimin kimligi.
    public Guid ServiceId { get; protected internal set; }

    // Istege bagli direction_id lookup satirinin kimligi.
    public int? TripDirectionId { get; protected internal set; }

    // Istege bagli wheelchair_accessible lookup satirinin kimligi.
    public int? WheelchairAccessibleId { get; protected internal set; }

    // Istege bagli bikes_allowed lookup satirinin kimligi.
    public int? BikesAllowedId { get; protected internal set; }

    // Istege bagli cars_allowed lookup satirinin kimligi.
    public int? CarsAllowedId { get; protected internal set; }

    // GTFS trip_id; veri seti icinde seferi tanimlayan kimlik.
    public string GtfsTripId { get; protected internal set; } = default!;

    // GTFS shape_id; seferin harita uzerinde izledigi sekil noktalarinin ortak kimligi.
    public string? GtfsShapeId { get; protected internal set; }

    // GTFS block_id; ayni aracin pespese isledigi seferleri gruplayan kimlik.
    public string? BlockId { get; protected internal set; }

    // GTFS trip_headsign; yolcuya gosterilen varis tabelasi.
    public string? Headsign { get; protected internal set; }

    // GTFS trip_short_name; yolcuya duyurulan kisa sefer adi.
    public string? ShortName { get; protected internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad: kisa ad, yoksa tabela; ikisi de yoksa trip_id. Kolon degildir.
    public string Name => ShortName ?? Headsign ?? GtfsTripId;

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Seferin isledigi guzergah.
    public Route Route { get; protected internal set; } = default!;
    // Seferin calistigi takvim servisi.
    public Service Service { get; protected internal set; } = default!;
    // Seferin guzergah uzerindeki yonu.
    public TripDirection? TripDirection { get; protected internal set; }
    // Sefere tekerlekli sandalye ile erisim durumu.
    public WheelchairBoarding? WheelchairAccessible { get; protected internal set; }
    // Seferde bisiklet tasinip tasinamayacagi.
    public BikesAllowed? BikesAllowed { get; protected internal set; }
    // Seferde arac tasinip tasinamayacagi.
    public CarsAllowed? CarsAllowed { get; protected internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Trip()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Trip(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }

    // Disa aktarimin bu kayda bakan yabanci anahtarlari cevirdigi GTFS kimligini public setter acmadan verir.
    string? IHasGtfsId.GtfsId => GtfsTripId;
}
