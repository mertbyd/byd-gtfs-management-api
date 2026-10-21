namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: areas.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Bolge hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS bolge hata kodlarini tanimlar.</summary>
public static class AreaExceptionCodes
{
    public const string AreaIdRequired = GtfsManagementErrorCodes.AreaGroup + "0001";
    public const string AreaIdTooLong = GtfsManagementErrorCodes.AreaGroup + "0002";
    public const string AreaNameTooLong = GtfsManagementErrorCodes.AreaGroup + "0003";
    public const string AreaAlreadyExists = GtfsManagementErrorCodes.AreaGroup + "0004";
}
