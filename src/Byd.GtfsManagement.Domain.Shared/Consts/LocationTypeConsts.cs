using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS durak konumu turu lookup tablosunun, indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin standart konum turlerini ayni sabitlerden okumasini saglar.
public static class LocationTypeConsts
{
    public const string TableName = "LocationTypes";
    public const string CodeIndex = "IX_LocationTypes_Code";

    public const int StopOrPlatformCode = 0;
    public const int StationCode = 1;
    public const int EntranceOrExitCode = 2;
    public const int GenericNodeCode = 3;
    public const int BoardingAreaCode = 4;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "Stop or platform", StopOrPlatformCode),
        (2, "Station", StationCode),
        (3, "Entrance or exit", EntranceOrExitCode),
        (4, "Generic node", GenericNodeCode),
        (5, "Boarding area", BoardingAreaCode)
    ];
}
