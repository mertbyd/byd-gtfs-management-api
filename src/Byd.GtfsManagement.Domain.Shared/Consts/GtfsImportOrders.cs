namespace Byd.GtfsManagement.Consts;

// islevi: GTFS tablolarinin ice aktarim yazim sirasini tanimlar; kucuk olan once yazilir.
// sistemdeki gorevi: Her ImportDto sirasini GtfsFile beyaninda buradan alir. Baska tabloya yabanci anahtarla bakan tablo,
// baktigi tablolarin en gec yazilanindan bir sonra yazilir; bakmayan tablo ilk turda yazilir.
public static class GtfsImportOrders
{
    public const int Agency = 0;
    public const int Level = 0;
    public const int Stop = Level + 1;
    public const int Route = Agency + 1;
    public const int Shape = 0;
    public const int Pathway = Stop + 1;
    public const int Area = 0;
    public const int StopArea = 0;
    public const int Network = 0;
    public const int RouteNetwork = 0;
    public const int LocationGroup = 0;
    public const int LocationGroupStop = 0;
    public const int Service = 0;
    public const int Calendar = Service + 1;
    public const int CalendarDate = Service + 1;
    public const int Trip = Route + 1;
    public const int StopTime = Trip + 1;
    public const int Frequency = Trip + 1;
    public const int Transfer = Trip + 1;
    public const int BookingRule = 0;
    public const int Timeframe = 0;
    public const int FareAttribute = Agency + 1;
    public const int FareRule = FareAttribute + 1;
    public const int RiderCategory = 0;
    public const int FareMedia = 0;
    public const int FareProduct = 0;
    public const int FareLegRule = 0;
    public const int FareLegJoinRule = 0;
    public const int FareTransferRule = 0;
    public const int FeedInfo = 0;
    public const int Attribution = Trip + 1;
    public const int Translation = 0;
}
