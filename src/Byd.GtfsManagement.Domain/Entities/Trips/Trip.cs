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
public class Trip : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS route_id; seferin isledigi guzergahin kimligi.
    public Guid RouteId { get; internal set; }

    // Seferin isledigi guzergah.
    public Route Route { get; internal set; } = default!;

    // GTFS service_id; seferin calistigi takvimin kimligi.
    public Guid ServiceId { get; internal set; }

    // Seferin calistigi takvim servisi.
    public Service Service { get; internal set; } = default!;

    // Istege bagli direction_id lookup satirinin kimligi.
    public int? TripDirectionId { get; internal set; }

    // Seferin guzergah uzerindeki yonu.
    public TripDirection? TripDirection { get; internal set; }

    // Istege bagli wheelchair_accessible lookup satirinin kimligi.
    public int? WheelchairAccessibleId { get; internal set; }

    // Sefere tekerlekli sandalye ile erisim durumu.
    public WheelchairBoarding? WheelchairAccessible { get; internal set; }

    // Istege bagli bikes_allowed lookup satirinin kimligi.
    public int? BikesAllowedId { get; internal set; }

    // Seferde bisiklet tasinip tasinamayacagi.
    public BikesAllowed? BikesAllowed { get; internal set; }

    // Istege bagli cars_allowed lookup satirinin kimligi.
    public int? CarsAllowedId { get; internal set; }

    // Seferde arac tasinip tasinamayacagi.
    public CarsAllowed? CarsAllowed { get; internal set; }

    // GTFS trip_id; veri seti icinde seferi tanimlayan kimlik.
    public string GtfsTripId { get; internal set; } = default!;

    // GTFS shape_id; seferin harita uzerinde izledigi sekil noktalarinin ortak kimligi.
    public string? GtfsShapeId { get; internal set; }

    // GTFS block_id; ayni aracin pespese isledigi seferleri gruplayan kimlik.
    public string? BlockId { get; internal set; }

    // GTFS trip_headsign; yolcuya gosterilen varis tabelasi.
    public string? Headsign { get; internal set; }

    // GTFS trip_short_name; yolcuya duyurulan kisa sefer adi.
    public string? ShortName { get; internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad: kisa ad, yoksa tabela; ikisi de yoksa trip_id. Kolon degildir.
    public string Name => ShortName ?? Headsign ?? GtfsTripId;

    // EF materialization icin bos kabuk kurar.
    protected Trip()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Trip(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
