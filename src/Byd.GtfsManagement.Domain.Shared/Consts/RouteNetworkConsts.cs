namespace Byd.GtfsManagement.Consts;

// islevi: route_networks.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS hat ag iliskisi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS hat ag iliskisi sabitlerini tanimlar.</summary>
public static class RouteNetworkConsts
{
    public const string TableName = "RouteNetworks";
    public const string KeyIndex = "IX_RouteNetworks_FeedId_GtfsRouteId";
    public const int MaxGtfsNetworkIdLength = 256;
    public const int MaxGtfsRouteIdLength = 256;
}
