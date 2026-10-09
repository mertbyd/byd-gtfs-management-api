namespace Byd.GtfsManagement.Permissions;

// islevi: Transfers okuma ve yonetim izinlerini tanimlar.
// sistemdeki gorevi: Endpoint erisim sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS aktarma izinlerini tanimlar.</summary>
public static partial class GtfsManagementPermissions
{
    // islevi: Transfer View ve Manage izin adlarini gruplar.
    // sistemdeki gorevi: Yetki agacina kararli bir aile sunar.
    /// <summary>GTFS aktarma izin ailesini tanimlar.</summary>
    public static class Transfers
    {
        public const string Default = GroupName + ".Transfers";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
