namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: networks.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ulasim agi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ulasim agi hata kodlarini tanimlar.</summary>
public static class NetworkExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.NetworkGroup + "0001";
    public const string NetworkAlreadyExists = GtfsManagementErrorCodes.NetworkGroup + "0002";
    public const string GtfsNetworkIdRequired = GtfsManagementErrorCodes.NetworkGroup + "0003";
    public const string GtfsNetworkIdTooLong = GtfsManagementErrorCodes.NetworkGroup + "0004";
    public const string NetworkNameTooLong = GtfsManagementErrorCodes.NetworkGroup + "0005";
}
