namespace Byd.GtfsManagement.Permissions;

// islevi: Level yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS kat erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Levels
    {
        public const string Default = GroupName + ".Levels";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
