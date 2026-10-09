using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Frequencies;

// islevi: frequencies.txt icindeki bir seferin servis sikligi araligini saklar.
// sistemdeki gorevi: Trip bagini ve zaman araligini ayni GTFS Feed'inde tutar.
/// <summary>Bir seferin GTFS siklik araligini temsil eder.</summary>
public class Frequency : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // frequencies.txt trip_id alaninin sistem kimligi.
    public Guid TripId { get; internal set; }

    // Siklik araliginin ait oldugu sefer.
    public Trip Trip { get; internal set; } = default!;

    // Ilk aracin ilk duraktan kalkis saati.
    public string StartTime { get; internal set; } = default!;

    // Siklik araliginin bittigi servis gunu saati.
    public string EndTime { get; internal set; } = default!;

    // Ayni duraktan kalkislar arasindaki saniye.
    public int HeadwaySecs { get; internal set; }

    // Hizmetin siklik veya kesin tarifeli olma kodu.
    public int? ExactTimes { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Frequency()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam alanlarini kurar.
    public Frequency(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
