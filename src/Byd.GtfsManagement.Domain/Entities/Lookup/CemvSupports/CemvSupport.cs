using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Entities.Lookup.CemvSupports;

// islevi: GTFS agency ve route cEMV destek kodlarini referans veri olarak tasir.
// sistemdeki gorevi: Enum alanlarini ortak int kimlik, ad ve GTFS koduyla esler.
public class CemvSupport : Entity<int>, ILookup
{
    // GTFS cEMV destek kodunun kullaniciya gorunen aciklamasi.
    public string Name { get; internal set; } = default!;

    // GTFS cemv_support alaninin 0, 1 veya 2 kodu.
    public int Code { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected CemvSupport()
    {
    }

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    internal CemvSupport(int id, string name, int code) : base(id)
    {
        Name = name;
        Code = code;
    }

    // ILookup yazma sozlesmesini public entity setter acmadan uygular.
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
