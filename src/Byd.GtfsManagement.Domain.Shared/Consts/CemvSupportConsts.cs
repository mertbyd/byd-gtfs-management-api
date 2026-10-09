using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: cEMV lookup tablosunun, kod indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni cEMV kodlarini okumasini saglar.
public static class CemvSupportConsts
{
    public const string TableName = "CemvSupports";
    public const string CodeIndex = "IX_CemvSupports_Code";

    public const int NoInformationCode = 0;
    public const int SupportedCode = 1;
    public const int NotSupportedCode = 2;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "No information", NoInformationCode),
        (2, "Supported", SupportedCode),
        (3, "Not supported", NotSupportedCode)
    ];
}
