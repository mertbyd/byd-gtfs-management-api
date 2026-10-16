namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: stop_areas.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Durak bolge iliskisi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS durak bolge iliskisi hata kodlarini tanimlar.</summary>
public static class StopAreaExceptionCodes
{
    public const string StopAreaAlreadyExists = GtfsManagementErrorCodes.StopAreaGroup + "0002";
    public const string GtfsAreaIdRequired = GtfsManagementErrorCodes.StopAreaGroup + "0003";
    public const string GtfsAreaIdTooLong = GtfsManagementErrorCodes.StopAreaGroup + "0004";
    public const string GtfsStopIdRequired = GtfsManagementErrorCodes.StopAreaGroup + "0005";
    public const string GtfsStopIdTooLong = GtfsManagementErrorCodes.StopAreaGroup + "0006";
}
