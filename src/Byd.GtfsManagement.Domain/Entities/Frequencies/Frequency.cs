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
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // frequencies.txt trip_id alaninin sistem kimligi.
    public Guid TripId { get; protected internal set; }

    // Ilk aracin ilk duraktan kalkis saati.
    public string StartTime { get; protected internal set; } = default!;

    // Siklik araliginin bittigi servis gunu saati.
    public string EndTime { get; protected internal set; } = default!;

    // Ayni duraktan kalkislar arasindaki saniye.
    public int HeadwaySecs { get; protected internal set; }

    // Hizmetin siklik veya kesin tarifeli olma kodu.
    public int? ExactTimes { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Siklik araliginin ait oldugu sefer.
    public Trip Trip { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Frequency()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam alanlarini kurar.
    public Frequency(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }
}
