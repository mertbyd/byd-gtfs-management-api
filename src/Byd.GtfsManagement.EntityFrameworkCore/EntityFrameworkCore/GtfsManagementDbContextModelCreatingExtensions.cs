using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: GTFS modelinin tuketici DbContext'lerine eklenecegi uzantiyi tanimlar.
// sistemdeki gorevi: Tablo kurallari konfigurasyon siniflarinda durdugu icin burada ek kural yoktur.
public static class GtfsManagementDbContextModelCreatingExtensions
{
    // GTFS modelini verilen model kurucusuna ekler.
    public static void ConfigureGtfsManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
