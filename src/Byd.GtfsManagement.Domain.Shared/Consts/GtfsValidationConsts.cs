using System;

namespace Byd.GtfsManagement.Consts;

// islevi: Kanonik GTFS dogrulayici, arsiv ve rapor sozlesmesinin sabitlerini tanimlar.
// sistemdeki gorevi: Application, Domain ve validator arasinda sabit metin tekrarini engeller.
public static class GtfsValidationConsts
{
    public const string ArchiveExtension = ".zip";
    public const string BaseUrlConfigurationKey = "GtfsValidator:BaseUrl";
    public const string HttpClientName = "GtfsValidator";
    public const string ValidateUploadPath = "validate-upload";
    public const string UploadFieldName = "file";
    public const string ProviderName = "MobilityData GTFS Schedule Validator";
    public const string MethodName = "ValidateUpload";
    public const string RequestType = "GTFS ZIP";
    public const string ErrorSeverity = "ERROR";
    public const string WarningSeverity = "WARNING";
    public const string InfoSeverity = "INFO";
    public const string UnprocessableFeedCode = "unprocessable_feed";
    public const string ZipMediaType = "application/zip";
    public const string JsonMediaType = "application/json";
    // Disa aktarilan raporun dosya adi: ad, zaman damgasi ve uzanti.
    public const string ReportFileName = "gtfs-validation-report-";
    public const string ReportTimestampFormat = "yyyyMMddHHmmss";
    public const string ReportFileExtension = ".json";
    public static readonly TimeSpan RequestTimeout = TimeSpan.FromMinutes(30);

    public static class Json
    {
        public const string Summary = "summary";
        public const string Notices = "notices";
        public const string ValidatorVersion = "validatorVersion";
        public const string ValidatedAt = "validatedAt";
        public const string Code = "code";
        public const string Severity = "severity";
        public const string TotalNotices = "totalNotices";
        public const string SampleNotices = "sampleNotices";
        // Ornek notice'larin konum alanlari kurala gore farkli adlar alir (filename, childFilename, fieldName1,
        // newCsvRowNumber); adinda bu parcayi buyuk-kucuk harf ayirmadan iceren ilk alan okunur.
        public const string FileNamePart = "filename";
        public const string FieldNamePart = "fieldname";
        public const string CsvRowNumberPart = "csvrownumber";
    }
}
