using System;
using System.Collections.Generic;

namespace Byd.GtfsManagement.Attributes;

// islevi: ImportDto'nun hangi GTFS dosyasindan okunup yazildigini ve tablolar arasindaki yazim sirasini bildirir.
// sistemdeki gorevi: Her ImportDto'da bulunur; sirayi GtfsImportOrders sabitinden alir. Ice aktarim tablolari bu sirayla
// dosyalarindan okur, disa aktarim ayni sirayla dosyasina yazar. Birden cok dosyadan turetilen tablonun ayni kaydi bir kez
// yazilir ve ayri dosya olarak disa aktarilmaz.
[AttributeUsage(AttributeTargets.Class)]
public sealed class GtfsFileAttribute(string[] fileNames, int importOrder) : Attribute
{
    // Tek dosyadan okunan tablo.
    public GtfsFileAttribute(string fileName, int importOrder) : this([fileName], importOrder)
    {
    }

    // Tablonun okundugu GTFS dosyalari.
    public IReadOnlyList<string> FileNames { get; } = fileNames;

    // Tablonun yazim sirasi; kucuk olan once yazilir.
    public int ImportOrder { get; } = importOrder;

    // Iki takvim dosyasindan turetilen servis gibi birden cok dosyadan olusan tablo.
    public bool IsDerived => FileNames.Count > 1;

    // Disa aktarimda yazilan dosya; birden cok dosyadan turetilen tablonun ayri dosyasi yoktur.
    public string? ExportFile => IsDerived ? null : FileNames[0];
}
