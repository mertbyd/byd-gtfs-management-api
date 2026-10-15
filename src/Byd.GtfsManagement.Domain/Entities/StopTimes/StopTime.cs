using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.LocationGroups;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Byd.GtfsManagement.Entities.Lookup.PickupDropOffTypes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.StopTimes;

// islevi: stop_times.txt icindeki bir seferin sirali durak ziyaretini saklar.
// sistemdeki gorevi: Sefer ve duragi ayni Feed kapsaminda zaman ve binis kurallariyla baglar.
/// <summary>Bir seferin tek durak ziyareti ve GTFS zaman bilgileri.</summary>
public class StopTime : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS trip_id alaninin sistemdeki sefer kimligi.
    public Guid TripId { get; protected internal set; }

    // GTFS stop_id alaninin sistemdeki durak kimligi; konum grubuna hizmet eden ziyarette bostur.
    public Guid? StopId { get; protected internal set; }

    // GTFS location_group_id; talep uzerine hizmet verilen konum grubunun kimligi.
    public Guid? LocationGroupId { get; protected internal set; }

    // GTFS pickup_type lookup satirinin kimligi.
    public int? PickupTypeId { get; protected internal set; }

    // GTFS drop_off_type lookup satirinin kimligi.
    public int? DropOffTypeId { get; protected internal set; }

    // Guzergah uzerindeki kesintisiz binis davranisinin lookup kimligi.
    public int? ContinuousPickupId { get; protected internal set; }

    // Guzergah uzerindeki kesintisiz inis davranisinin lookup kimligi.
    public int? ContinuousDropOffId { get; protected internal set; }

    // GTFS pickup_booking_rule_id; binis rezervasyon kuralinin booking_rule_id degeri.
    public string? PickupBookingRuleId { get; protected internal set; }

    // GTFS drop_off_booking_rule_id; inis rezervasyon kuralinin booking_rule_id degeri.
    public string? DropOffBookingRuleId { get; protected internal set; }

    // Sefer icinde kesin artan GTFS stop_sequence degeri.
    public int StopSequence { get; protected internal set; }

    // Servis gunu icinde 24 saati asabilen varis saati.
    public string? ArrivalTime { get; protected internal set; }

    // Servis gunu icinde 24 saati asabilen kalkis saati.
    public string? DepartureTime { get; protected internal set; }

    // Bu duraga ozel yolcu tabelasi.
    public string? StopHeadsign { get; protected internal set; }

    // Talep uzerine hizmet penceresinin baslangici.
    public string? StartPickupDropOffWindow { get; protected internal set; }

    // Talep uzerine hizmet penceresinin bitisi.
    public string? EndPickupDropOffWindow { get; protected internal set; }

    // Sekil boyunca bu duraga kadar kat edilen mesafe.
    public double? ShapeDistTraveled { get; protected internal set; }

    // Saatlerin kesin (1) veya yaklasik (0) oldugunu belirtir.
    public int? Timepoint { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Durak zamaninin bagli oldugu sefer.
    public Trip Trip { get; protected internal set; } = default!;
    // Ziyaret edilen durak veya peron.
    public Stop? Stop { get; protected internal set; }
    // Talep uzerine hizmet verilen konum grubu.
    public LocationGroup? LocationGroup { get; protected internal set; }
    // Duraktaki binis davranisi.
    public PickupDropOffType? PickupType { get; protected internal set; }
    // Duraktaki inis davranisi.
    public PickupDropOffType? DropOffType { get; protected internal set; }
    // Guzergah uzerindeki kesintisiz binis davranisi.
    public ContinuousPickupDropOff? ContinuousPickup { get; protected internal set; }
    // Guzergah uzerindeki kesintisiz inis davranisi.
    public ContinuousPickupDropOff? ContinuousDropOff { get; protected internal set; }

    // EF materialization icin bos veri kabugu kurar.
    protected StopTime()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam alanlarini kurar.
    public StopTime(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }
}
