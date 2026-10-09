using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: stop_times.txt pickup_type ve drop_off_type kodlarini tanimlar.
// sistemdeki gorevi: Iki alanin paylastigi GTFS kodlarini sabit lookup satirlarina baglar.
/// <summary>GTFS binis ve inis kodlarini tanimlar.</summary>
public static class PickupDropOffTypeConsts
{
    public const string TableName = "PickupDropOffTypes";
    public const string CodeIndex = "IX_PickupDropOffTypes_Code";
    public const int RegularCode = 0;
    public const int NoServiceCode = 1;
    public const int PhoneAgencyCode = 2;
    public const int CoordinateWithDriverCode = 3;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "Regularly scheduled", RegularCode),
        (2, "Not available", NoServiceCode),
        (3, "Must phone agency", PhoneAgencyCode),
        (4, "Must coordinate with driver", CoordinateWithDriverCode)
    ];
}
