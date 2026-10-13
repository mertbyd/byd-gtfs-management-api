using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Entities.Lookup.PickupDropOffTypes;

// islevi: pickup_type ve drop_off_type kodlarini referans veri olarak tasir.
// sistemdeki gorevi: GTFS kodlarini kalici int Id, Name ve Code lookup kayitlariyla esler.
/// <summary>Binis ve inis GTFS kodlarini saklar.</summary>
public class PickupDropOffType : Entity<int>, ILookup
{
    // GTFS durak binis-inis kodunun kullaniciya gorunen adi.
    public string Name { get; internal set; } = default!;

    // GTFS pickup_type ve drop_off_type alaninin kodu.
    public int Code { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected PickupDropOffType()
    {
    }

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    internal PickupDropOffType(int id, string name, int code) : base(id)
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
