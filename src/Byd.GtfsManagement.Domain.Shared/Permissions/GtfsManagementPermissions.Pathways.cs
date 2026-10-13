namespace Byd.GtfsManagement.Permissions;

// islevi: Pathway yetki anahtarlarini tanimlar.
// sistemdeki gorevi: GTFS yuruyus yolu erisimini goruntuleme ve yonetim yetkilerine ayirir.
public static partial class GtfsManagementPermissions
{
    public static class Pathways
    {
        public const string Default = GroupName + ".Pathways";
        public const string View = Default + ".View";
        public const string Manage = Default + ".Manage";
    }
}
