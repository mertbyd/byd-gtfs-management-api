using System;

namespace Byd.GtfsManagement.Attributes;

// islevi: GTFS dosyasindaki bir kolonun hangi tablonun anahtarini veya lookup kodunu tasidigini bildirir.
// sistemdeki gorevi: Ice aktarim DTO'larinda kolonun hedef tablosunu tek yerde beyan eder; CSV okuyucu anahtari bu tabloyla
// etiketler, mapper Feed'e ozgu kimligi veya lookup kimligini bu tabloya gore uretir.
[AttributeUsage(AttributeTargets.Property)]
public sealed class GtfsReferenceAttribute(string target) : Attribute
{
    // Kolonun anahtarini tasidigi tablonun adi.
    public string Target { get; } = target;
}
