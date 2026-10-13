using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS bikes_allowed lookup tablosunun, kod indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni bisiklet kodlarini okumasini saglar.
public static class BikesAllowedConsts
{
    public const string TableName = "BikesAlloweds";
    public const string CodeIndex = "IX_BikesAlloweds_Code";

    public const int NoInformationCode = 0;
    public const int AllowedCode = 1;
    public const int NotAllowedCode = 2;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "No bicycle information", NoInformationCode),
        (2, "Bicycles allowed", AllowedCode),
        (3, "Bicycles not allowed", NotAllowedCode)
    ];
}
