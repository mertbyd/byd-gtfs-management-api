using System;

namespace Byd.GtfsManagement.Consts;

// islevi: Kanonik GTFS dogrulayici, arsiv ve rapor sozlesmesinin sabitlerini tanimlar.
// sistemdeki gorevi: Application, Domain ve validator arasinda sabit metin tekrarini engeller.
public static class GtfsValidationConsts
{
    public const string ArchiveExtension = ".zip";
    // CLI host'un calistigi klasordeki bu alt klasorde, surum ekine bagli olmayan bu adla durur.
    public const string CliDirectoryName = "gtfs-validator";
    public const string CliFileName = "gtfs-validator-cli.jar";
    public const string JavaHomeVariable = "JAVA_HOME";
    public const string JavaBinDirectoryName = "bin";
    public const string JavaExecutable = "java";
    public const string WorkDirectoryPrefix = "gtfs-validation-";
    public const string OutputDirectoryName = "output";
    public const string ReportJsonFileName = "report.json";
    public const string SystemErrorsJsonFileName = "system_errors.json";
    public const string ProviderName = "MobilityData GTFS Schedule Validator";
    public const string MethodName = "ValidateArchive";
    public const string RequestType = "GTFS ZIP";
    public const string ErrorSeverity = "ERROR";
    public const string WarningSeverity = "WARNING";
    public const string InfoSeverity = "INFO";
    public const string ZipMediaType = "application/zip";
    public const string JsonMediaType = "application/json";
    // Disa aktarilan raporun dosya adi: ad, zaman damgasi ve uzanti.
    public const string ReportFileName = "gtfs-validation-report-";
    public const string ReportTimestampFormat = "yyyyMMddHHmmss";
    public const string ReportFileExtension = ".json";
    public static readonly TimeSpan ValidationTimeout = TimeSpan.FromMinutes(30);

    // MobilityData CLI'inin kullanilan secenekleri.
    public static class Cli
    {
        public const string Jar = "-jar";
        public const string Input = "-i";
        public const string Output = "-o";
        public const string SkipValidatorUpdate = "-svu";
    }

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
