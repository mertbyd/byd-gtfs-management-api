using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS continuous_pickup ve continuous_drop_off lookup tablosunun sozlesmesini tutar.
// sistemdeki gorevi: Iki alan ayni kod kumesini kullandigi icin tek referans tablosunu paylasirlar.
public static class ContinuousPickupDropOffConsts
{
    public const string TableName = "ContinuousPickupDropOffs";
    public const string CodeIndex = "IX_ContinuousPickupDropOffs_Code";

    public const int ContinuousCode = 0;
    public const int NotContinuousCode = 1;
    public const int PhoneAgencyCode = 2;
    public const int CoordinateWithDriverCode = 3;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "Continuous stopping", ContinuousCode),
        (2, "No continuous stopping", NotContinuousCode),
        (3, "Must phone agency", PhoneAgencyCode),
        (4, "Must coordinate with driver", CoordinateWithDriverCode)
    ];
}
