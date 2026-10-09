using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Levels;

// islevi: levels.txt istasyon kat bilgilerini saklar.
// sistemdeki gorevi: Durak ve yuruyus yolu baglantilarinin dikey konumunu belirleyen katlari tutar.
/// <summary>GTFS istasyon katini temsil eder.</summary>
public class Level : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS level_id; kati tanimlayan dis kimlik.
    public string GtfsLevelId { get; internal set; } = default!;

    // Katin sayisal yukseklik dizini (or: 0 zemin, -1 bodrum, 1 birinci kat).
    public double LevelIndex { get; internal set; }

    // Katin yolculara gosterilen istege bagli adi.
    public string? Name { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Level()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Level(Guid id, Guid? tenantId, Guid feedId, string gtfsLevelId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsLevelId = gtfsLevelId;
    }
}
