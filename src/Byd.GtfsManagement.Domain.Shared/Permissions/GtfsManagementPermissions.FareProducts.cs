namespace Byd.GtfsManagement.Permissions;

// islevi: FareProducts yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS ucret urunu erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class FareProducts
    {
        public const string Default = GroupName + ".FareProducts";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
