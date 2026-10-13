namespace Byd.GtfsManagement.Consts;

// islevi: fare_leg_join_rules.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS ucret etap birlestirme kurali sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ucret etap birlestirme kurali sabitlerini tanimlar.</summary>
public static class FareLegJoinRuleConsts
{
    public const string TableName = "FareLegJoinRules";
    public const string KeyIndex = "IX_FareLegJoinRules_FeedId";
    public const int MaxFromNetworkIdLength = 256;
    public const int MaxToNetworkIdLength = 256;
    public const int MaxFromStopIdLength = 256;
    public const int MaxToStopIdLength = 256;
}
