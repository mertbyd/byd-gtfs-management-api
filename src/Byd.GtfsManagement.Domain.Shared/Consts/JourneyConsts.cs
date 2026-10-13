namespace Byd.GtfsManagement.Consts;

// islevi: Yolculuk planlamasinin aktarma, arama penceresi ve yurume sinirlarini tanimlar.
// sistemdeki gorevi: Planlayici ve girdi dogrulamasi ayni sinirlari okur.
public static class JourneyConsts
{
    public const int DefaultMaxTransfers = 2;
    public const int MaxTransfersLimit = 4;
    public const int SearchWindowSeconds = 4 * 60 * 60;
    public const int MinChangeSeconds = 60;
    public const int StationTransferSeconds = 120;
    public const int MaxFootpathSeconds = 15 * 60;
    public const double MaxWalkMeters = 400;
    public const double WalkMetersPerSecond = 1.2;
    public const int SecondsPerDay = 24 * 60 * 60;
}
