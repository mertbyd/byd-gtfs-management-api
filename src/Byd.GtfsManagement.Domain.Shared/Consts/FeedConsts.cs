namespace Byd.GtfsManagement.Consts;

// islevi: Feed tablosunun, alan sinirlarinin, ZIP dosya kategorisinin ve entegrasyon iscisinin sabit sozlesmesini tanimlar.
// sistemdeki gorevi: Domain, EF, validator, seeder ve entegrasyon iscisi ayni Feed sozlesmesini okur.
public static class FeedConsts
{
    public const int MaxNameLength = 200;
    public const int MaxNameWordLength = 20;
    public const string TableName = "Feeds";
    // Yayindaki Feed verisini okuyan uclarin yanit onbellegi grubu; Feed entity'sinin tip adidir, Feed degisince bu gruptaki
    // yanitlar silinir.
    public const string ResponseCacheGroup = "Feed";
    public const string ScopeNameIndex = "IX_Feeds_Tenant_Organization_Name";
    public const string ActiveOrganizationIndex = "IX_Feeds_Tenant_Organization_Active";
    public const string ValidationReportColumnType = "jsonb";
    public const int MaxUrlLength = 2048;
    public const int MaxSourceUrlLength = MaxUrlLength;
    public const int MaxStoredFileNameLength = 255;
    public const int MaxFailureCodeLength = 64;

    // File modulunde GTFS ZIP'lerinin saklandigi kategori; modul kategoriyi kendisi seed eder.
    public const string ArchiveFileCategoryName = "GTFS feed archives";

    // Dogrulanmis Feed'leri veritabanina yazan iscinin tarama araligi ve birden fazla instance'ta tek calismasini saglayan kilit.
    public const int IntegrationScanPeriodSeconds = 500;
    public const string IntegrationLockName = "GtfsManagement.FeedIntegration";
    public const string TurkishLanguageCode = "tr";
    public const string EnglishLanguageCode = "en";

    // Ice aktarim hatasinda lokalize mesaja tasinan dosya, satir, alan, lookup ve kod bilgilerinin anahtarlari.
    public const string FileDataKey = "file";
    public const string RowDataKey = "row";
    public const string FieldDataKey = "field";
    public const string LookupDataKey = "lookup";
    public const string CodeDataKey = "code";
    // Dogrulamada hata urettigi icin kaydedilmeyen degisikligin hata sayisini ve hata kayitlarini tasiyan anahtarlar.
    public const string ErrorCountDataKey = "errorCount";
    public const string ErrorsDataKey = "errors";
    // GTFS CSV'lerinde mantiksal degerlerin yazimi.
    public const string BooleanTrueValue = "1";
    public const string BooleanFalseValue = "0";

    public const string HttpClientName = "GtfsFeedDownloader";
    public const long MaxArchiveBytes = 512L * 1024 * 1024;
    public static readonly System.TimeSpan RequestTimeout = System.TimeSpan.FromMinutes(10);
    public const string ProviderName = "GTFS Feed Source";
    public const string MethodName = "DownloadArchive";
    public const string RequestType = "GET";
}
