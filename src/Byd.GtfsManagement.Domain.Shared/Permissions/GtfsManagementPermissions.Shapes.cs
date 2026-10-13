namespace Byd.GtfsManagement.Permissions;

// islevi: Shape yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS sekil erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Shapes
    {
        public const string Default = GroupName + ".Shapes";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
