namespace Byd.GtfsManagement.Consts;

// islevi: transfers.txt tablo, indeks ve kapali transfer_type kodlarini tanimlar.
// sistemdeki gorevi: Transfer kurallari ile EF anahtarini ayni GTFS sozlesmesine baglar.
/// <summary>GTFS aktarma sabitlerini tanimlar.</summary>
public static class TransferConsts
{
    public const string TableName = "Transfers";
    public const string KeyIndex = "IX_Transfers_Feed_Endpoints";
    public const int RecommendedCode = 0;
    public const int TimedCode = 1;
    public const int MinimumTimeCode = 2;
    public const int NotPossibleCode = 3;
    public const int InSeatAllowedCode = 4;
    public const int InSeatForbiddenCode = 5;
}
