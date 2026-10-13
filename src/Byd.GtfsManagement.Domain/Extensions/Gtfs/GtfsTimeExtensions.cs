using System;
using System.Globalization;

namespace Byd.GtfsManagement.Extensions.Gtfs;

/// <summary>GTFS servis gunu saat karsilastirmasi.</summary>
// islevi: 24 saati asabilen HH:MM:SS degerlerini servis gunu saatine gore karsilastirir.
// sistemdeki gorevi: Siklik araliklari ve talep penceresi gibi saat sirasi kurallari ayni karsilastirmayi kullanir.
public static class GtfsTimeExtensions
{
    // GTFS saatini servis gunu baslangicindan itibaren gecen saniyeye cevirir.
    public static int ToServiceSeconds(this string time)
    {
        var parts = time.Split(':');
        return int.Parse(parts[0], CultureInfo.InvariantCulture) * 3600
            + int.Parse(parts[1], CultureInfo.InvariantCulture) * 60
            + int.Parse(parts[2], CultureInfo.InvariantCulture);
    }

    // Servis gunu saniyesini 24 saati asabilen GTFS HH:MM:SS bicimine cevirir.
    public static string ToGtfsTime(this int seconds)
        => string.Create(CultureInfo.InvariantCulture, $"{seconds / 3600:00}:{seconds / 60 % 60:00}:{seconds % 60:00}");

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
