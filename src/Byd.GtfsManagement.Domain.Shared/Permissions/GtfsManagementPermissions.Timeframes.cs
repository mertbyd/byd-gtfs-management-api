namespace Byd.GtfsManagement.Permissions;

// islevi: Timeframes yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS zaman araligi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Timeframes
    {
        public const string Default = GroupName + ".Timeframes";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
