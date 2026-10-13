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
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Aktarmanin basladigi durak.
    public Guid? FromStopId { get; internal set; }

    // Aktarmanin basladigi durak kaydi.
    public Stop? FromStop { get; internal set; }

    // Aktarmanin bittigi durak.
    public Guid? ToStopId { get; internal set; }

    // Aktarmanin bittigi durak kaydi.
    public Stop? ToStop { get; internal set; }

    // Baslangic guzergahi.
    public Guid? FromRouteId { get; internal set; }

    // Baslangic guzergah kaydi.
    public Route? FromRoute { get; internal set; }

    // Varis guzergahi.
    public Guid? ToRouteId { get; internal set; }

    // Varis guzergah kaydi.
    public Route? ToRoute { get; internal set; }

    // Aktarmanin basladigi sefer.
    public Guid? FromTripId { get; internal set; }

    // Baslangic sefer kaydi.
    public Trip? FromTrip { get; internal set; }

    // Aktarmanin bittigi sefer.
    public Guid? ToTripId { get; internal set; }

    // Varis sefer kaydi.
    public Trip? ToTrip { get; internal set; }

    // GTFS transfer_type kodu.
    public int TransferType { get; internal set; }

    // Istege bagli asgari aktarma suresi.
    public int? MinTransferTime { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Transfer()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam alanlarini kurar.
    public Transfer(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
