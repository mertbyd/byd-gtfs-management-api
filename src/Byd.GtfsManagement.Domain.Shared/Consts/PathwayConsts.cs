namespace Byd.GtfsManagement.Consts;

// islevi: pathways.txt tablo adi, indeks, mod ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS yuruyus yolu sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS yuruyus yolu sabitlerini tanimlar.</summary>
public static class PathwayConsts
{
    public const string TableName = "Pathways";
    public const string KeyIndex = "IX_Pathways_FeedId_GtfsPathwayId";
    public const int MaxGtfsPathwayIdLength = 256;
    public const int MaxSignpostedAsLength = 256;

    // GTFS pathway_mode kodlari
    public const int Walkway = 1;
    public const int Stairs = 2;
    public const int MovingSidewalk = 3;
    public const int Escalator = 4;
    public const int Elevator = 5;
    public const int FareGate = 6;
    public const int ExitGate = 7;

    // GTFS is_bidirectional kodlari
    public const int Unidirectional = 0;
    public const int Bidirectional = 1;
}
