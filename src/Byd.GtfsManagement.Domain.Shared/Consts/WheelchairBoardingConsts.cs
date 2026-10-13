using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS tekerlekli sandalye erisimi lookup tablosunun, kod indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni wheelchair_boarding kodlarini okumasini saglar.
public static class WheelchairBoardingConsts
{
    public const string TableName = "WheelchairBoardings";
    public const string CodeIndex = "IX_WheelchairBoardings_Code";

    public const int NoInformationCode = 0;
    public const int AccessibleCode = 1;
    public const int NotAccessibleCode = 2;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "No accessibility information", NoInformationCode),
        (2, "Accessible", AccessibleCode),
        (3, "Not accessible", NotAccessibleCode)
    ];
}
