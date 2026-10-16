namespace Byd.GtfsManagement.Consts;

// islevi: Modulun veritabani sema adini derleme guvenli tek sabitte tutar.
// sistemdeki gorevi: Sema adinin tek kaynagidir; DbProperties ve EF konfigurasyonlari ayni sabiti okur.
public static class DatabaseConsts
{
    // GTFS modulunun kendi tablolarinin yazildigi sema.
    public const string ModuleSchema = "gtfs_management";

    // Arama indeksi adini organizasyon kimliginden ayiran karakter; her organizasyonun aktif Feed'i kendi indeksindedir.
    public const char SearchIndexOrganizationSeparator = '-';
}
