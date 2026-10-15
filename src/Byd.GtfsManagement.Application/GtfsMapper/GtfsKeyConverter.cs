using System;
using System.Collections.Generic;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Feeds;

namespace Byd.GtfsManagement.GtfsMapper;

// islevi: GtfsReference tasiyan kolonda GTFS anahtarini Feed kapsamli kayit kimligine, lookup kodunu lookup kimligine cevirir;
// yazarken kayit kimligini orijinal GTFS anahtarina, lookup kimligini GTFS koduna geri cevirir.
// sistemdeki gorevi: Kimlik cevirisinin tek yeridir; ImportDto kolonlari entity ile ayni tipte (Guid, int) oldugu icin Mapperly
// yalniz ad eslesmesiyle esler. Ceviri bilgisini arsiv baglamindan alir.
internal sealed class GtfsKeyConverter(GtfsArchiveContext archive) : DefaultTypeConverter
{
    private readonly Dictionary<MemberInfo, string> targets = [];

    // Guid kolonunda kayit kimligini, int kolonunda lookup kimligini verir; bos istege bagli hucre bos kalir.
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var optional = Nullable.GetUnderlyingType(memberMapData.Type);
        if (string.IsNullOrEmpty(text) && optional is not null)
            return null;
        var key = new GtfsKey(TargetOf(memberMapData.Member!), text ?? string.Empty);
        return (optional ?? memberMapData.Type) == typeof(Guid) ? archive.ToId(key) : archive.ToLookupId(key);
    }

    // Kayit kimligini GTFS anahtarina, lookup kimligini GTFS koduna cevirir; bos deger bos hucre olur, null hucreyi atlatip
    // kolonlari kaydirir.
    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData) => value switch
    {
        Guid id => archive.ToGtfsId(id),
        int id => archive.ToLookupCode(TargetOf(memberMapData.Member!), id),
        _ => string.Empty
    };

    // Kolonun hedef tablosunu GtfsReference beyanindan bir kez okur.
    private string TargetOf(MemberInfo member)
        => targets.TryGetValue(member, out var target)
            ? target
            : targets[member] = member.GetCustomAttribute<GtfsReferenceAttribute>()!.Target;
}
