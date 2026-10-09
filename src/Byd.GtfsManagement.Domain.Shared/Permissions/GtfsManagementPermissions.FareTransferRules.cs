namespace Byd.GtfsManagement.Permissions;

// islevi: FareTransferRules yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ucret aktarma kurali erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FareTransferRules
    {
        public const string Default = GroupName + ".FareTransferRules";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
