namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: transfers.txt alan ve iliski hatalarini kodlar.
// sistemdeki gorevi: Aktarma kurallarini lokalize edilebilir hata ailesine baglar.
/// <summary>GTFS aktarma hata kodlarini tanimlar.</summary>
public static class TransferExceptionCodes
{
    public const string FromStopRequired = GtfsManagementErrorCodes.TransferGroup + "0002";
    public const string ToStopRequired = GtfsManagementErrorCodes.TransferGroup + "0003";
    public const string FromTripRequired = GtfsManagementErrorCodes.TransferGroup + "0004";
    public const string ToTripRequired = GtfsManagementErrorCodes.TransferGroup + "0005";
    public const string TypeInvalid = GtfsManagementErrorCodes.TransferGroup + "0006";
    public const string MinimumTimeInvalid = GtfsManagementErrorCodes.TransferGroup + "0007";
    public const string MinimumTimeRequired = GtfsManagementErrorCodes.TransferGroup + "0008";
    public const string ReferenceOutOfFeed = GtfsManagementErrorCodes.TransferGroup + "0009";
    public const string StopTypeInvalid = GtfsManagementErrorCodes.TransferGroup + "0010";
    public const string RouteTripMismatch = GtfsManagementErrorCodes.TransferGroup + "0011";
    public const string DuplicateRule = GtfsManagementErrorCodes.TransferGroup + "0012";
    public const string LinkedServiceMismatch = GtfsManagementErrorCodes.TransferGroup + "0013";
}
