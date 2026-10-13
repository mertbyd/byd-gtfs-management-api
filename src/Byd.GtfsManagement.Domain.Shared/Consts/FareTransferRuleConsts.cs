namespace Byd.GtfsManagement.Consts;

// islevi: fare_transfer_rules.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ucret aktarma kurali sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ucret aktarma kurali sabitlerini tanimlar.</summary>
public static class FareTransferRuleConsts
{
    public const string TableName = "FareTransferRules";
    public const string KeyIndex = "IX_FareTransferRules_FeedId";
    public const int MaxFromLegGroupIdLength = 256;
    public const int MaxToLegGroupIdLength = 256;
    public const int MaxGtfsFareProductIdLength = 256;
}
