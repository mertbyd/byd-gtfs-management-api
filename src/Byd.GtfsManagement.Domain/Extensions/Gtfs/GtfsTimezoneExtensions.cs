using System;
using Volo.Abp;

namespace Byd.GtfsManagement.Extensions.Gtfs;

// islevi: GTFS saat dilimi alanlarinin bilinen bir IANA bolgesi oldugunu dogrular.
// sistemdeki gorevi: agency.txt ve stops.txt ayni kurali uyguladigi icin kontrol tek ada tasindi; hata kodu
// cagiran varliga ait oldugu icin disaridan verilir.
public static class GtfsTimezoneExtensions
{
    // Zorunlu saat diliminin tanimli bir IANA bolgesi oldugunu dogrular.
    public static string EnsureKnownTimezone(this string timezone, string errorCode)
    {
        if (!TimeZoneInfo.TryFindSystemTimeZoneById(timezone, out _))
            throw new BusinessException(errorCode);
        return timezone;
    }

    // Istege bagli saat dilimi verilmisse tanimli bir IANA bolgesi oldugunu dogrular.
    public static string? EnsureKnownTimezoneOrNull(this string? timezone, string errorCode)
        => timezone?.EnsureKnownTimezone(errorCode);
}
