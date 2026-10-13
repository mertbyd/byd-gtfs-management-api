using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS stop_access lookup tablosunun, kod indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni durak erisimi kodlarini okumasini saglar.
public static class StopAccessConsts
{
    public const string TableName = "StopAccesses";
    public const string CodeIndex = "IX_StopAccesses_Code";

    public const int ViaStationCode = 0;
    public const int DirectCode = 1;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "Accessed through the station", ViaStationCode),
        (2, "Accessed directly from the street", DirectCode)
    ];
}
