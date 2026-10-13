using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Pathways;

// islevi: pathways.txt istasyon ici yuruyus yollarini ve gecis modlarini saklar.
// sistemdeki gorevi: Duraklar ve peronlar arasindaki fiziki yuruyus ve aktarma yollarini tutar.
/// <summary>GTFS yuruyus yolunu temsil eder.</summary>
public class Pathway : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS pathway_id; yolu tanimlayan dis kimlik.
    public string GtfsPathwayId { get; internal set; } = default!;

    // Yolun basladigi durak veya konum.
    public Guid FromStopId { get; internal set; }

    // Baslangic duragi kaydi.
    public Stop FromStop { get; internal set; } = default!;

    // Yolun bittigi durak veya konum.
    public Guid ToStopId { get; internal set; }

    // Varis duragi kaydi.
    public Stop ToStop { get; internal set; } = default!;

    // GTFS pathway_mode; yolun fiziksel turu (1: walkway, 2: stairs vb.).
    public int PathwayMode { get; internal set; }

    // GTFS is_bidirectional; yolun cift yonlu kullanilip kullanilamayacagi (0/1).
    public int IsBidirectional { get; internal set; }

    // Yatay uzunluk (metre).
    public double? Length { get; internal set; }

    // Ortalama gecis suresi (saniye).
    public int? TraversalTime { get; internal set; }

    // Basamak sayisi.
    public int? StairCount { get; internal set; }

    // Maksimum egim orani.
    public double? MaxSlope { get; internal set; }

    // Asgari yol genisligi (metre).
    public double? MinWidth { get; internal set; }

    // Yon tabelasinda yazan metin.
    public string? SignpostedAs { get; internal set; }

    // Ters yondeki tabela metni.
    public string? ReversedSignpostedAs { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Pathway()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Pathway(Guid id, Guid? tenantId, Guid feedId, string gtfsPathwayId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsPathwayId = gtfsPathwayId;
    }
}
