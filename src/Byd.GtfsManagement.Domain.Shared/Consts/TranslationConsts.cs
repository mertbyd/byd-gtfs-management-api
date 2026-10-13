namespace Byd.GtfsManagement.Consts;

// islevi: translations.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ceviri sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ceviri sabitlerini tanimlar.</summary>
public static class TranslationConsts
{
    public const string TableName = "Translations";
    public const string KeyIndex = "IX_Translations_FeedId_Table_Field_Lang_Target";
    public const int MaxTableNameLength = 64;
    public const int MaxFieldNameLength = 64;
    public const int MaxLanguageLength = 10;
    public const int MaxRecordIdLength = 256;
    public const int MaxRecordSubIdLength = 256;
    public const int MaxTranslationTextLength = 2048;
    public const int MaxFieldValueLength = 2048;
}
