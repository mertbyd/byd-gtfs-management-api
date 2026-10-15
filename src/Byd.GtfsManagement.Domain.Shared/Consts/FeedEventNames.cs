namespace Byd.GtfsManagement.Consts;

// islevi: GTFS Feed entegrasyon olayinin sabit tel adini tanimlar.
// sistemdeki gorevi: Dogrulama yayincisi ile dagitik entegrasyon handler'inin ayni olay sozlesmesini kullanmasini saglar.
public static class FeedEventNames
{
    public const string IntegrationRequested = "GtfsManagement.Feeds.IntegrationRequested";
    public const string Changed = "GtfsManagement.Feeds.Changed";
}
