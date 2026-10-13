namespace Byd.GtfsManagement.Permissions;

// islevi: StopAreas yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS durak bolge iliskisi erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class StopAreas
    {
        public const string Default = GroupName + ".StopAreas";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
