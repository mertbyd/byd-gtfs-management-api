using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareTransferRules;

// islevi: fare_transfer_rules.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Ucret aktarma kurali verilerini Feed altinda tutar.
/// <summary>GTFS ucret aktarma kurali kaydini temsil eder.</summary>
public class FareTransferRule : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Kalkis etap grubu dis kimligi.
    public string? FromLegGroupId { get; protected internal set; }

    // Varis etap grubu dis kimligi.
    public string? ToLegGroupId { get; protected internal set; }

    // GTFS fare_product_id dis kimligi.
    public string? GtfsFareProductId { get; protected internal set; }

    // Izin verilen aktarma adedi (-1: sinirsiz).
    public int? TransferCount { get; protected internal set; }

    // Aktarma gecerlilik suresi (saniye).
    public int? DurationLimit { get; protected internal set; }

    // Sure siniri hesaplama tipi.
    public int? DurationLimitType { get; protected internal set; }

    // Aktarma ucretlendirme tipi (0: Toplam, 1: Ilk bilet + aktarma, 2: Sadece aktarma).
    public int FareTransferType { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected FareTransferRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareTransferRule(Guid id, Guid feedId, int fareTransferType) : base(id)
    {
        FeedId = feedId;
        FareTransferType = fareTransferType;
    }
}
