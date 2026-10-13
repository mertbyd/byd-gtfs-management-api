using System.Collections.Generic;

namespace Byd.GtfsManagement.Consts;

// islevi: GTFS direction_id lookup tablosunun, kod indeksinin ve sabit tanimlarinin sozlesmesini tutar.
// sistemdeki gorevi: Seed akisi ve EF eslemesinin ayni yon kodlarini okumasini saglar.
public static class TripDirectionConsts
{
    public const string TableName = "TripDirections";
    public const string CodeIndex = "IX_TripDirections_Code";

    public const int OutboundCode = 0;
    public const int InboundCode = 1;

    public static readonly IReadOnlyList<(int Id, string Name, int Code)> Definitions =
    [
        (1, "Outbound", OutboundCode),
        (2, "Inbound", InboundCode)
    ];
}
