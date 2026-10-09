namespace Byd.GtfsManagement.Permissions;

// islevi: Attributions yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS atif erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Attributions
    {
        public const string Default = GroupName + ".Attributions";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
