using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Levels;

// islevi: levels.txt istasyon kat bilgilerini saklar.
// sistemdeki gorevi: Durak ve yuruyus yolu baglantilarinin dikey konumunu belirleyen katlari tutar.
/// <summary>GTFS istasyon katini temsil eder.</summary>
public class Level : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS level_id; kati tanimlayan dis kimlik.
    public string GtfsLevelId { get; protected internal set; } = default!;

    // Katin sayisal yukseklik dizini (or: 0 zemin, -1 bodrum, 1 birinci kat).
    public double LevelIndex { get; protected internal set; }

    // Katin yolculara gosterilen istege bagli adi.
    public string? Name { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Level()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Level(Guid id, Guid feedId, string gtfsLevelId) : base(id)
    {
        FeedId = feedId;
        GtfsLevelId = gtfsLevelId;
    }
    // Dis anahtarlar disa aktarimda orijinal GTFS kimligine cozulur.
    string? IHasGtfsId.GtfsId => GtfsLevelId;
}
