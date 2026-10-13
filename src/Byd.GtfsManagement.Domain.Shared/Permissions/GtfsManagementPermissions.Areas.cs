namespace Byd.GtfsManagement.Permissions;

// islevi: Areas yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS bolge erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Areas
    {
        public const string Default = GroupName + ".Areas";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
