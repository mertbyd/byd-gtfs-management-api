namespace Byd.GtfsManagement.Permissions;

// islevi: Networks yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ulasim agi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Networks
    {
        public const string Default = GroupName + ".Networks";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
