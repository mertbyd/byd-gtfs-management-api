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
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Yolun basladigi durak veya konum.
    public Guid FromStopId { get; protected internal set; }

    // Yolun bittigi durak veya konum.
    public Guid ToStopId { get; protected internal set; }

    // GTFS pathway_id; yolu tanimlayan dis kimlik.
    public string GtfsPathwayId { get; protected internal set; } = default!;

    // GTFS pathway_mode; yolun fiziksel turu (1: walkway, 2: stairs vb.).
    public int PathwayMode { get; protected internal set; }

    // GTFS is_bidirectional; yolun cift yonlu kullanilip kullanilamayacagi (0/1).
    public int IsBidirectional { get; protected internal set; }

    // Yatay uzunluk (metre).
    public double? Length { get; protected internal set; }

    // Ortalama gecis suresi (saniye).
    public int? TraversalTime { get; protected internal set; }

    // Basamak sayisi.
    public int? StairCount { get; protected internal set; }

    // Maksimum egim orani.
    public double? MaxSlope { get; protected internal set; }

    // Asgari yol genisligi (metre).
    public double? MinWidth { get; protected internal set; }

    // Yon tabelasinda yazan metin.
    public string? SignpostedAs { get; protected internal set; }

    // Ters yondeki tabela metni.
    public string? ReversedSignpostedAs { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Baslangic duragi kaydi.
    public Stop FromStop { get; protected internal set; } = default!;
    // Varis duragi kaydi.
    public Stop ToStop { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Pathway()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Pathway(Guid id, Guid feedId, string gtfsPathwayId) : base(id)
    {
        FeedId = feedId;
        GtfsPathwayId = gtfsPathwayId;
    }
}
