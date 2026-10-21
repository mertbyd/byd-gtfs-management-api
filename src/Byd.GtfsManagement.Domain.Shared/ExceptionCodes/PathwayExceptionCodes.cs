namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: pathways.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Yuruyus yolu hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS yuruyus yolu hata kodlarini tanimlar.</summary>
public static class PathwayExceptionCodes
{
    public const string PathwayRequired = GtfsManagementErrorCodes.PathwayGroup + "0001";
    public const string PathwayTooLong = GtfsManagementErrorCodes.PathwayGroup + "0002";
    public const string FromStopRequired = GtfsManagementErrorCodes.PathwayGroup + "0003";
    public const string ToStopRequired = GtfsManagementErrorCodes.PathwayGroup + "0004";
    public const string SameStopForbidden = GtfsManagementErrorCodes.PathwayGroup + "0005";
    public const string ModeInvalid = GtfsManagementErrorCodes.PathwayGroup + "0006";
    public const string BidirectionalInvalid = GtfsManagementErrorCodes.PathwayGroup + "0007";
    public const string LengthInvalid = GtfsManagementErrorCodes.PathwayGroup + "0008";
    public const string TraversalTimeInvalid = GtfsManagementErrorCodes.PathwayGroup + "0009";
    public const string MinWidthInvalid = GtfsManagementErrorCodes.PathwayGroup + "0010";
    public const string SignpostedTooLong = GtfsManagementErrorCodes.PathwayGroup + "0011";
    public const string PathwayAlreadyExists = GtfsManagementErrorCodes.PathwayGroup + "0012";
    public const string StopOutOfFeed = GtfsManagementErrorCodes.PathwayGroup + "0014";
}
