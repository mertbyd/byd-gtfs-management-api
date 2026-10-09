namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: Yolculuk planlama girdisi ve kurallari icin lokalize hata kodlarini tanimlar.
// sistemdeki gorevi: Validator ve planlayici ayni hata sozlesmesini kullanir.
public static class JourneyExceptionCodes
{
    public const string FromStopRequired = GtfsManagementErrorCodes.JourneyGroup + "0001";
    public const string ToStopRequired = GtfsManagementErrorCodes.JourneyGroup + "0002";
    public const string SameStopForbidden = GtfsManagementErrorCodes.JourneyGroup + "0003";
    public const string DateRequired = GtfsManagementErrorCodes.JourneyGroup + "0004";
    public const string TimeInvalid = GtfsManagementErrorCodes.JourneyGroup + "0005";
    public const string MaxTransfersInvalid = GtfsManagementErrorCodes.JourneyGroup + "0006";
    public const string StopsInDifferentFeeds = GtfsManagementErrorCodes.JourneyGroup + "0007";
}
