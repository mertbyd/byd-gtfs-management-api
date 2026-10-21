namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: shapes.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Sekil hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS sekil hata kodlarini tanimlar.</summary>
public static class ShapeExceptionCodes
{
    public const string ShapeRequired = GtfsManagementErrorCodes.ShapeGroup + "0001";
    public const string ShapeTooLong = GtfsManagementErrorCodes.ShapeGroup + "0002";
    public const string LatitudeInvalid = GtfsManagementErrorCodes.ShapeGroup + "0003";
    public const string LongitudeInvalid = GtfsManagementErrorCodes.ShapeGroup + "0004";
    public const string SequenceInvalid = GtfsManagementErrorCodes.ShapeGroup + "0005";
    public const string DistTraveledInvalid = GtfsManagementErrorCodes.ShapeGroup + "0006";
    public const string PointAlreadyExists = GtfsManagementErrorCodes.ShapeGroup + "0007";
}
