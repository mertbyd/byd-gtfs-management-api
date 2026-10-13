using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;

// islevi: continuous_pickup ve continuous_drop_off kodlarini referans veri olarak tasir.
// sistemdeki gorevi: GTFS kodlarini kalici int Id, Name ve Code lookup kayitlariyla esler.
public class ContinuousPickupDropOff : Entity<int>, ILookup
{
    // GTFS yol boyu binis-inis kodunun kullaniciya gorunen adi.
    public string Name { get; internal set; } = default!;

    // GTFS continuous_pickup ve continuous_drop_off alaninin kodu.
    public int Code { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected ContinuousPickupDropOff()
    {
    }

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    internal ContinuousPickupDropOff(int id, string name, int code) : base(id)
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
