namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: translations.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ceviri hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ceviri hata kodlarini tanimlar.</summary>
public static class TranslationExceptionCodes
{
    public const string TableNameRequired = GtfsManagementErrorCodes.TranslationGroup + "0001";
    public const string TableNameTooLong = GtfsManagementErrorCodes.TranslationGroup + "0002";
    public const string FieldNameRequired = GtfsManagementErrorCodes.TranslationGroup + "0003";
    public const string FieldNameTooLong = GtfsManagementErrorCodes.TranslationGroup + "0004";
    public const string LanguageRequired = GtfsManagementErrorCodes.TranslationGroup + "0005";
    public const string LanguageTooLong = GtfsManagementErrorCodes.TranslationGroup + "0006";
    public const string TranslationRequired = GtfsManagementErrorCodes.TranslationGroup + "0007";
    public const string TranslationTooLong = GtfsManagementErrorCodes.TranslationGroup + "0008";
    public const string TargetRequired = GtfsManagementErrorCodes.TranslationGroup + "0009";
    public const string RecordIdTooLong = GtfsManagementErrorCodes.TranslationGroup + "0010";
    public const string RecordSubIdTooLong = GtfsManagementErrorCodes.TranslationGroup + "0011";
    public const string FieldValueTooLong = GtfsManagementErrorCodes.TranslationGroup + "0012";
    public const string TranslationAlreadyExists = GtfsManagementErrorCodes.TranslationGroup + "0013";
    public const string FeedRequired = GtfsManagementErrorCodes.TranslationGroup + "0014";
}
