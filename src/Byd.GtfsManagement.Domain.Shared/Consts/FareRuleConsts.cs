namespace Byd.GtfsManagement.Consts;

// islevi: fare_rules.txt tablo adi, indeks ve bolge kimlik sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ucret kurali sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ucret kurali sabitlerini tanimlar.</summary>
public static class FareRuleConsts
{
    public const string TableName = "FareRules";
    public const string KeyIndex = "IX_FareRules_FeedId_FareAttributeId";
    public const int MaxZoneIdLength = 256;
}
