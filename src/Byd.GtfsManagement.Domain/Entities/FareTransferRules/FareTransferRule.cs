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
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Kalkis etap grubu dis kimligi.
    public string? FromLegGroupId { get; internal set; }

    // Varis etap grubu dis kimligi.
    public string? ToLegGroupId { get; internal set; }

    // GTFS fare_product_id dis kimligi.
    public string? GtfsFareProductId { get; internal set; }

    // Izin verilen aktarma adedi (-1: sinirsiz).
    public int? TransferCount { get; internal set; }

    // Aktarma gecerlilik suresi (saniye).
    public int? DurationLimit { get; internal set; }

    // Sure siniri hesaplama tipi.
    public int? DurationLimitType { get; internal set; }

    // Aktarma ucretlendirme tipi (0: Toplam, 1: Ilk bilet + aktarma, 2: Sadece aktarma).
    public int FareTransferType { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected FareTransferRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareTransferRule(Guid id, Guid? tenantId, Guid feedId, int fareTransferType) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        FareTransferType = fareTransferType;
    }
}
