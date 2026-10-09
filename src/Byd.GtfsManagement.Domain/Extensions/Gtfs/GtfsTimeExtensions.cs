using System;

namespace Byd.GtfsManagement.Extensions.Gtfs;

/// <summary>GTFS servis gunu saat karsilastirmasi.</summary>
// islevi: 24 saati asabilen HH:MM:SS degerlerini servis gunu saatine gore karsilastirir.
// sistemdeki gorevi: Siklik araliklari ve talep penceresi gibi saat sirasi kurallari ayni karsilastirmayi kullanir.
public static class GtfsTimeExtensions
{
    // Iki GTFS saatini servis gunu saat degerine gore karsilastirir.
    public static int CompareGtfsTimes(this string first, string second)
    {
        var firstSeparator = first.IndexOf(':');
        var secondSeparator = second.IndexOf(':');
        var firstHours = first.AsSpan(0, firstSeparator).TrimStart('0');
        var secondHours = second.AsSpan(0, secondSeparator).TrimStart('0');
        var hourComparison = firstHours.Length.CompareTo(secondHours.Length);
        if (hourComparison == 0)
            hourComparison = firstHours.CompareTo(secondHours, StringComparison.Ordinal);
        return hourComparison != 0
            ? hourComparison
            : first.AsSpan(firstSeparator).CompareTo(second.AsSpan(secondSeparator), StringComparison.Ordinal);
    }
}
