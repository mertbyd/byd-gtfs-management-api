namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: levels.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Kat hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS kat hata kodlarini tanimlar.</summary>
public static class LevelExceptionCodes
{
    public const string LevelRequired = GtfsManagementErrorCodes.LevelGroup + "0001";
    public const string LevelTooLong = GtfsManagementErrorCodes.LevelGroup + "0002";
    public const string IndexInvalid = GtfsManagementErrorCodes.LevelGroup + "0003";
    public const string NameTooLong = GtfsManagementErrorCodes.LevelGroup + "0004";
    public const string LevelAlreadyExists = GtfsManagementErrorCodes.LevelGroup + "0005";
    public const string FeedRequired = GtfsManagementErrorCodes.LevelGroup + "0006";
}
