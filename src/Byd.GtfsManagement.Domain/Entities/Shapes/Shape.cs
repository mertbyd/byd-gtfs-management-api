using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Shapes;

// islevi: shapes.txt icindeki bir sekil noktasini saklar.
// sistemdeki gorevi: Seferlerin harita uzerindeki fiziki cizgisini sira numarasiyla olusturan noktalari tutar.
/// <summary>Bir GTFS sekil noktasini temsil eder.</summary>
public class Shape : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS shape_id; sekli tanimlayan dis kimlik.
    public string GtfsShapeId { get; protected internal set; } = default!;

    // Sekil noktasinin enlemi (WGS 84).
    public double Latitude { get; protected internal set; }

    // Sekil noktasinin boylami (WGS 84).
    public double Longitude { get; protected internal set; }

    // Sekil cizgisi uzerindeki sira numarasi.
    public int Sequence { get; protected internal set; }

    // Sekil baslangicindan bu noktaya kadar kat edilen toplam mesafe.
    public double? DistTraveled { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Shape()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Shape(Guid id, Guid feedId, string gtfsShapeId) : base(id)
    {
        FeedId = feedId;
        GtfsShapeId = gtfsShapeId;
    }
}
