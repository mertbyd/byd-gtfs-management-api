using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Entities.Lookup.TripDirections;

// islevi: direction_id kodlarini referans veri olarak tasir.
// sistemdeki gorevi: GTFS kodlarini kalici int Id, Name ve Code lookup kayitlariyla esler.
public class TripDirection : Entity<int>, ILookup
{
    // GTFS sefer yonu kodunun kullaniciya gorunen adi.
    public string Name { get; internal set; } = default!;

    // GTFS direction_id alaninin kodu.
    public int Code { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected TripDirection()
    {
    }

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    internal TripDirection(int id, string name, int code) : base(id)
    {
        Name = name;
        Code = code;
    }

    // ILookup ad yazma sozlesmesini public entity setter acmadan uygular.
    string ILookup.Name
    {
        get => Name;
        set => Name = value;
    }

    // ILookup kod yazma sozlesmesini public entity setter acmadan uygular.
    int? ILookup.Code
    {
        get => Code;
        set => Code = value ?? throw new ArgumentNullException(nameof(value));
    }
}
