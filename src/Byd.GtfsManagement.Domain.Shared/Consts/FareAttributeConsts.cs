namespace Byd.GtfsManagement.Consts;

// islevi: fare_attributes.txt tablo adi, indeks, odeme ve aktarma kodlarini tanimlar.
// sistemdeki gorevi: GTFS ucret ozelligi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS ucret ozelligi sabitlerini tanimlar.</summary>
public static class FareAttributeConsts
{
    public const string TableName = "FareAttributes";
    public const string KeyIndex = "IX_FareAttributes_FeedId_GtfsFareId";
    public const int MaxGtfsFareIdLength = 256;
    public const int MaxCurrencyTypeLength = 3;

    // GTFS payment_method kodlari
    public const int PaymentMethodPaidOnBoard = 0;
    public const int PaymentMethodPaidBeforeBoarding = 1;

    // GTFS transfers kodlari
    public const int TransfersNone = 0;
    public const int TransfersOnce = 1;
    public const int TransfersTwice = 2;
}
