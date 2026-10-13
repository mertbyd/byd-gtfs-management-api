using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS calendar_dates exception_type lookup tablosunun sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni istisna kodlarini okumasini saglar.
public static class ExceptionTypeConsts
{
    public const string TableName = "ExceptionTypes";
    public const string CodeIndex = "IX_ExceptionTypes_Code";

    public const int ServiceAddedCode = 1;
    public const int ServiceRemovedCode = 2;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "Service added for the date", ServiceAddedCode),
        (2, "Service removed for the date", ServiceRemovedCode)
    ];
}
