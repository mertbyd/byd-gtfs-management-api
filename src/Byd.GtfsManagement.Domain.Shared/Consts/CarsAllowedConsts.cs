using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS cars_allowed lookup tablosunun, kod indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni arac tasima kodlarini okumasini saglar.
public static class CarsAllowedConsts
{
    public const string TableName = "CarsAlloweds";
    public const string CodeIndex = "IX_CarsAlloweds_Code";

    public const int NoInformationCode = 0;
    public const int AllowedCode = 1;
    public const int NotAllowedCode = 2;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "No car information", NoInformationCode),
        (2, "Cars allowed", AllowedCode),
        (3, "Cars not allowed", NotAllowedCode)
    ];
}
