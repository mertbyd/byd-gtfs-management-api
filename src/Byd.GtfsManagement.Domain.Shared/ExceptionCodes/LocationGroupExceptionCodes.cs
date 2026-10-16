namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: location_groups.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Konum grubu hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS konum grubu hata kodlarini tanimlar.</summary>
public static class LocationGroupExceptionCodes
{
    public const string LocationGroupAlreadyExists = GtfsManagementErrorCodes.LocationGroupGroup + "0002";
    public const string GtfsLocationGroupIdRequired = GtfsManagementErrorCodes.LocationGroupGroup + "0003";
    public const string GtfsLocationGroupIdTooLong = GtfsManagementErrorCodes.LocationGroupGroup + "0004";
    public const string LocationGroupNameTooLong = GtfsManagementErrorCodes.LocationGroupGroup + "0005";
}
