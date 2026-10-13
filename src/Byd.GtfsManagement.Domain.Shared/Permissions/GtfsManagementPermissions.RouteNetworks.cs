namespace Byd.GtfsManagement.Permissions;

// islevi: RouteNetworks yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS hat ag iliskisi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class RouteNetworks
    {
        public const string Default = GroupName + ".RouteNetworks";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
