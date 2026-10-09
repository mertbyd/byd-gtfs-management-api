using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Entities.Lookup.LocationTypes;

// islevi: stops.txt location_type kodlarini referans veri olarak tasir.
// sistemdeki gorevi: GTFS kodlarini kalici int Id, Name ve Code lookup kayitlariyla esler.
public class LocationType : Entity<int>, ILookup
{
    // Konum turunun kullaniciya gorunen adi.
    public string Name { get; internal set; } = default!;

    // GTFS location_type alaninin 0 ile 4 arasindaki kodu.
    public int Code { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected LocationType()
    {
    }

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    internal LocationType(int id, string name, int code) : base(id)
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
