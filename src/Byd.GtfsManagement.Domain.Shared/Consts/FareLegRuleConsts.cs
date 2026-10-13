namespace Byd.GtfsManagement.Consts;

// islevi: fare_leg_rules.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ucret etap kurali sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ucret etap kurali sabitlerini tanimlar.</summary>
public static class FareLegRuleConsts
{
    public const string TableName = "FareLegRules";
    public const string KeyIndex = "IX_FareLegRules_FeedId_GtfsFareProductId";
    public const int MaxLegGroupIdLength = 256;
    public const int MaxNetworkIdLength = 256;
    public const int MaxFromAreaIdLength = 256;
    public const int MaxToAreaIdLength = 256;
    public const int MaxFromTimeframeGroupIdLength = 256;
    public const int MaxToTimeframeGroupIdLength = 256;
    public const int MaxGtfsFareProductIdLength = 256;
}
