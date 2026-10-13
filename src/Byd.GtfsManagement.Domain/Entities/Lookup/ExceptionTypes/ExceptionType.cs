using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Entities.Lookup.ExceptionTypes;

// islevi: calendar_dates.txt exception_type kodlarini referans veri olarak tasir.
// sistemdeki gorevi: GTFS kodlarini kalici int Id, Name ve Code lookup kayitlariyla esler.
public class ExceptionType : Entity<int>, ILookup
{
    // Istisna turunun kullaniciya gorunen adi.
    public string Name { get; internal set; } = default!;

    // GTFS exception_type alaninin 1 veya 2 kodu.
    public int Code { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected ExceptionType()
    {
    }

    // Tanimli GTFS kodunu sabit lookup kimligiyle kurar.
    internal ExceptionType(int id, string name, int code) : base(id)
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
