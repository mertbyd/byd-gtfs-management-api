namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: GTFS service_id kayitlarinin is kurali hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: Servis kimligi ihlallerini lokalize edilebilir kodlara baglar.
public static class ServiceExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.ServiceGroup + "0001";
    public const string GtfsServiceIdRequired = GtfsManagementErrorCodes.ServiceGroup + "0002";
    public const string GtfsServiceIdTooLong = GtfsManagementErrorCodes.ServiceGroup + "0003";
}
