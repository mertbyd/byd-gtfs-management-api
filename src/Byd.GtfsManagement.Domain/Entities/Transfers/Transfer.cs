using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Transfers;

// islevi: transfers.txt aktarma noktasini ve sefer/guzergah ozellesmesini saklar.
// sistemdeki gorevi: GTFS aktarma kuralini tek Feed ve iliskili kayitlar altinda tutar.
/// <summary>Bir GTFS aktarma kuralini temsil eder.</summary>
public class Transfer : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Aktarmanin basladigi durak.
    public Guid? FromStopId { get; protected internal set; }

    // Aktarmanin bittigi durak.
    public Guid? ToStopId { get; protected internal set; }

    // Baslangic guzergahi.
    public Guid? FromRouteId { get; protected internal set; }

    // Varis guzergahi.
    public Guid? ToRouteId { get; protected internal set; }

    // Aktarmanin basladigi sefer.
    public Guid? FromTripId { get; protected internal set; }

    // Aktarmanin bittigi sefer.
    public Guid? ToTripId { get; protected internal set; }

    // GTFS transfer_type kodu.
    public int TransferType { get; protected internal set; }

    // Istege bagli asgari aktarma suresi.
    public int? MinTransferTime { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Aktarmanin basladigi durak kaydi.
    public Stop? FromStop { get; protected internal set; }
    // Aktarmanin bittigi durak kaydi.
    public Stop? ToStop { get; protected internal set; }
    // Baslangic guzergah kaydi.
    public Route? FromRoute { get; protected internal set; }
    // Varis guzergah kaydi.
    public Route? ToRoute { get; protected internal set; }
    // Baslangic sefer kaydi.
    public Trip? FromTrip { get; protected internal set; }
    // Varis sefer kaydi.
    public Trip? ToTrip { get; protected internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Transfer()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam alanlarini kurar.
    public Transfer(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }
}
